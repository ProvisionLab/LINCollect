using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Web.Common;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.DTO;
using Web.Models.ViewModels;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class PollController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IPublishSurveyManager _publishSurveyManager;
        private readonly IQuestionAnswerManager _questionAnswerManager;
        private readonly IResultManager _resultManager;
        private readonly ISectionManager _sectionManager;
        private readonly ISurveyManager _surveyManager;

        public PollController(IGoogleSheetsService googleSheetsService,
            ApplicationDbContext dbContext,
            IEmailService emailService,
            ISurveyManager surveyManager,
            IPublishSurveyManager publishSurveyManager,
            IQuestionAnswerManager questionAnswerManager,
            IResultManager resultManager,
            ISectionManager sectionManager)
        {
            _googleSheetsService = googleSheetsService;
            _dbContext = dbContext;
            _emailService = emailService;
            _surveyManager = surveyManager;
            _publishSurveyManager = publishSurveyManager;
            _questionAnswerManager = questionAnswerManager;
            _resultManager = resultManager;
            _sectionManager = sectionManager;
        }

        [HttpGet]
        public ActionResult Pass(Guid id)
        {
            var publishSurvey =
                _dbContext.PublishSurveys.Include(s => s.Survey).FirstOrDefault(s => s.Link == id.ToString());

            if (publishSurvey == null || publishSurvey.IsPassed)
                return HttpNotFound();

            ViewBag.Title = publishSurvey.Survey.Name;

            var surveyId = publishSurvey.SurveyId;

            if (surveyId < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var survey = _dbContext.Surveys.Find(surveyId);
            if (survey == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new PreviewView();
            model.UserLinkId = id;
            model.SurveyId = survey.Id;
            model.SurveyName = survey.Name;
            model.IntroductionText = survey.Introduction;
            model.ThanksText = survey.Thanks;
            model.Banner = survey.Banner;

            model.AboutYouBefore = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => !x.IsAfterSurvey));
            model.AboutYouAfter = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey));

            model.Items = Mapper.Map<List<RelationshipItemModel>>(survey.RelationshipItems?.OrderBy(x => x.OrderId).ToList());

            foreach (var item in model.Items)
            {
                var fileId = int.Parse(item.NodeList);
                var file = _dbContext.SurveyFiles.Find(fileId);

                if (file == null)
                {
                    item.Companies = new Companies { RelationshipId = item.Id, Names = new List<CompanyItem>() };
                }
                else
                {
                    var companies = new List<CompanyItem>();
                    var error = "";

                    foreach (var companyName in _googleSheetsService.GetCompanies(file.Link, ref error))
                    {
                        companies.Add(new CompanyItem { Name = companyName, Checked = false });
                    }
                    var _companies = new Companies
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = companies,
                        Error = !string.IsNullOrEmpty(error) ? string.Format(error, file.Name, file.Link) : string.Empty
                    };
                    if (item.SortNodeList)
                        _companies.Names = _companies.Names.OrderBy(x => x.Name).ToList();

                    item.Companies = _companies;
                }
            }
            ViewData["Passing"] = true;
            ViewBag.Passing = true;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Pass(PollResultView pollResult)
        {
            var publishSurvey = await _publishSurveyManager.GetByGuidAsync(pollResult.UserLinkId);

            if (publishSurvey == null)
            {
                return HttpNotFound();
            }

            var result = new ResultModel
            {
                PassDate = DateTime.Now,
                PublishSurveyId = publishSurvey.Id
            };

            var resultId = await _resultManager.InsertAsync(result);
            var sectionType = await _sectionManager.GetAsync();

            //Before
            if (pollResult.AboutYouBefore != null && pollResult.AboutYouBefore.QuestionAnswers != null)
            {
                var sectionId = await _resultManager.InsertSection(
                    new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.RespondentBefore).Id, SectionId = pollResult.AboutYouBefore.Id });

                foreach (var questionAnswerModel in pollResult.AboutYouBefore.QuestionAnswers)
                {
                    questionAnswerModel.ResultSectionId = sectionId;
                    await _questionAnswerManager.InsertAsync(questionAnswerModel);
                }
            }
            //After
            if (pollResult.AboutYouAfter?.QuestionAnswers != null)
            {
                var sectionId = await _resultManager.InsertSection(
                   new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.RespondentAfter).Id, SectionId = pollResult.AboutYouBefore.Id });

                foreach (var questionAnswerModel in pollResult.AboutYouAfter.QuestionAnswers)
                {
                    questionAnswerModel.ResultSectionId = sectionId;
                    await _questionAnswerManager.InsertAsync(questionAnswerModel);
                }
            }
            if (pollResult.Items != null && pollResult.Items.Count > 0)
            {


                //Relationships
                foreach (var relationShip in pollResult.Items)
                {
                    if (relationShip.QuestionAnswers != null)
                    {
                        var sectionId = await _resultManager.InsertSection(
                            new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.Relationship).Id, SectionId = relationShip.Id });


                        foreach (var questionAnswerModel in relationShip.QuestionAnswers)
                        {
                            questionAnswerModel.ResultSectionId = sectionId;
                            await _questionAnswerManager.InsertAsync(questionAnswerModel);
                        }
                    }
                    if (relationShip.NQuestionAnswers != null)
                    {
                        var sectionId = await _resultManager.InsertSection(
                            new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.Node).Id, SectionId = relationShip.Id });


                        foreach (var questionAnswerModel in relationShip.NQuestionAnswers)
                        {
                            questionAnswerModel.ResultSectionId = sectionId;
                            await _questionAnswerManager.InsertAsync(questionAnswerModel);
                        }
                    }
                }
            }

            publishSurvey.IsPassed = true;
            await _publishSurveyManager.UpdateAsync(publishSurvey);

            //Save logic

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Publish(int id)
        {
            var survey = await _dbContext.Surveys.FindAsync(id);
            if (survey == null)
                return HttpNotFound();

            ViewBag.SurveyTitle = survey.Name;

            var model = new PublishView();
            model.SurveyId = survey.Id;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Submit(PublishView publish)
        {
            if (publish.Subject == null || publish.Message == null || publish.SurveyId < 1)
                return HttpNotFound();

            var survey =
                _dbContext.Surveys.Where(s => s.Id == publish.SurveyId).Include(s => s.SurveyFile).FirstOrDefault();

            var error = "";

            var enumerators = await _googleSheetsService.GetEnumerators(survey.SurveyFile.Link, ref error);

            var submitView = new SubmitView();

            foreach (var enumerator in enumerators)
            {
                var message = new MailMessage();
                message.To.Add(enumerator[1]);
                message.Subject = publish.Subject;
                message.Body = publish.Message;
                message.IsBodyHtml = true;
                message.From = new MailAddress("lincollect@gmail.com", "LINCollect");

                var publishGuid = Guid.NewGuid().ToString();

                var pollUrl = $"{Request.Url.Scheme}{Uri.SchemeDelimiter}{Request.Url.Authority}/poll/pass/{publishGuid}";

                if (message.Body.Contains("[link]"))
                {
                    message.Body = message.Body.Replace("[link]", $"{pollUrl}");
                }
                else if (message.Body.Contains("[link:"))
                {
                    var index = message.Body.IndexOf("[link:");
                    var colIndex = message.Body.IndexOf(":", index);
                    var bracketIndex = message.Body.IndexOf("]", colIndex);
                    var linkTitle = message.Body.Substring(colIndex + 1, bracketIndex - colIndex - 1);

                    message.Body = message.Body.Substring(0, index)
                                   + $"<a href=\"{pollUrl}\">{linkTitle}</a>" +
                                   message.Body.Substring(bracketIndex + 1);
                }
                else
                {
                    message.Body += pollUrl;
                }

                var sent = await _emailService.SendAsync(message);

                var model = new PublishSurveyModel
                {
                    UserName = enumerator[0],
                    UserEmail = enumerator[1],
                    Succeed = sent
                };

                if (sent)
                {
                    model.SurveyId = publish.SurveyId;
                    model.Link = publishGuid;
                    await _publishSurveyManager.InsertAsync(model);
                }

                submitView.Succeed.Add(model);
            }

            await _surveyManager.Publish(survey.Id);

            return View(submitView);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return RedirectToAction("Index", "Surveys");
        }
    }
}