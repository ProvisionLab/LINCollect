using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.DTO;
using Web.Models.ViewModels;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class PollController : Controller
    {
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ISurveyManager _surveyManager;
        private readonly IPublishSurveyManager _publishSurveyManager;

        public PollController(IGoogleSheetsService googleSheetsService, ApplicationDbContext dbContext, IEmailService emailService, ISurveyManager surveyManager, IPublishSurveyManager publishSurveyManager)
        {
            _googleSheetsService = googleSheetsService;
            _dbContext = dbContext;
            _emailService = emailService;
            _surveyManager = surveyManager;
            _publishSurveyManager = publishSurveyManager;
        }

        [HttpGet]
        public ActionResult Pass(Guid id)
        {
            var publishSurvey = _dbContext.PublishSurveys.Include(s => s.Survey).FirstOrDefault(s => s.Link == id.ToString());

            if (publishSurvey == null)
            {
                return HttpNotFound();
            }

            var surveyId = publishSurvey.SurveyId;

            if (surveyId < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var survey = _dbContext.Surveys.Find(surveyId);
            if (survey == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new PreviewView();
            model.SurveyId = survey.Id;
            model.SurveyName = survey.Name;
            model.IntroductionText = survey.Introduction;
            model.ThanksText = survey.Thanks;
            model.Banner = survey.Banner;

            model.AboutYouBefore = survey.Respondents.FirstOrDefault(x => !x.IsAfterSurvey);
            model.AboutYouAfter = survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey);

            model.Items = survey.RelationshipItems?.OrderBy(x => x.OrderId).ToList();
            model.Companies = new List<Companies>();

            foreach (var item in model.Items)
            {
                var fileId = int.Parse(item.NodeList);
                var file = _dbContext.SurveyFiles.Find(fileId);

                if (file == null)
                    model.Companies.Add(new Companies() { RelationshipId = item.Id, Names = new List<string>() });
                else
                {
                    var error = "";
                    var _companies = new Companies()
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = _googleSheetsService.GetCompanies(file.Link, ref error),
                        Error = !String.IsNullOrEmpty(error) ? String.Format(error, file.Name, file.Link) : String.Empty
                    };
                    if (item.SortNodeList)
                        _companies.Names = _companies.Names.OrderBy(x => x).ToList();

                    model.Companies.Add(_companies);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Publish(int id)
        {
            var survey = await _dbContext.Surveys.FindAsync(id);
            if (survey == null)
            {
                return HttpNotFound();
            }

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
            {
                return HttpNotFound();
            }

            var survey = _dbContext.Surveys.Where(s => s.Id == publish.SurveyId).Include(s => s.SurveyFile).FirstOrDefault();

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
                    model.Link = Guid.NewGuid().ToString();
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