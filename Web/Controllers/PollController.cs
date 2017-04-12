using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.DTO;
using Web.Models.ViewModels;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PollController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IPublishSurveyManager _publishSurveyManager;
        private readonly INQuestionManager _nQuestionManager;
        private readonly ISurveyManager _surveyManager;

        public PollController(IGoogleSheetsService googleSheetsService,
            ApplicationDbContext dbContext,
            IEmailService emailService,
            ISurveyManager surveyManager,
            IPublishSurveyManager publishSurveyManager,
            INQuestionManager nQuestionManager
        )
        {
            _googleSheetsService = googleSheetsService;
            _dbContext = dbContext;
            _emailService = emailService;
            _surveyManager = surveyManager;
            _publishSurveyManager = publishSurveyManager;
            _nQuestionManager = nQuestionManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Pass(Guid id)
        {
            var model = await _surveyManager.GetPreview(id);

            if (model == null)
                return HttpNotFound();

            ViewBag.Title = model.SurveyName;

            ViewData["Passing"] = true;
            ViewBag.Passing = true;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetNQuestions(int relationshipId, int index)
        {
            var nodeQuestions =await _nQuestionManager.GetByRelationship(relationshipId);
            ViewData.Add("base", $"Items[{index}]");
            ViewData.Add("id", relationshipId);
            ViewData.Add("region", "Html");
            return PartialView("NQuestions", nodeQuestions);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Pass(PollResultView pollResult)
        {
            var publishSurvey = await _publishSurveyManager.GetByGuidAsync(pollResult.UserLinkId);

            if (publishSurvey == null)
                return HttpNotFound();
            var result = await _surveyManager.Submit(publishSurvey.Id, pollResult);

            if (result)
            {
                publishSurvey.IsPassed = true;
                await _publishSurveyManager.UpdateAsync(publishSurvey);
                return RedirectToAction("Thanks");
            }
            //Save logic
            return RedirectToAction("Pass", new {id = publishSurvey.Link});
        }

        [HttpGet]
        public async Task<ActionResult> Publish(int id)
        {
            var survey = await _dbContext.Surveys.FindAsync(id);
            if (survey == null)
                return HttpNotFound();

            ViewBag.SurveyTitle = survey.Name;
            ViewBag.Title = "Overview";

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

                //var pollUrl =
                //    $"{Request.Url.Scheme}{Uri.SchemeDelimiter}{Request.Url.Authority}/poll/pass/{publishGuid}";
                var pollUrl =
                      $"{Request.Url.Scheme}{Uri.SchemeDelimiter}{Request.Url.Host}:40083/poll/pass/{publishGuid}";


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


        [HttpGet]
        public ActionResult Thanks()
        {
            ViewData["Passing"] = true;
            ViewBag.Passing = true;
            return View();
        }
    }
}