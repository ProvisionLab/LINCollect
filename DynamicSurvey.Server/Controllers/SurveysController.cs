using System;
using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Services;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using Survey = DynamicSurvey.Server.DAL.Entities.Survey;

namespace DynamicSurvey.Server.Controllers
{
    public class SurveysController : Controller
    {
        private readonly SurveyService _surveyService;
        private readonly ISurveysRepository _surveysRepository;

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveyService = new SurveyService();
            _surveysRepository = surveysRepository;
        }

        public ActionResult Index()
        {

            var res = _surveysRepository.GetSurveys(Session.GetCurrentUser());
            return View(new SurveysViewModel
            {
                Surveys = res
            });
        }

        public ActionResult CopySurvey(ulong sourceId)
        {
            var survey = _surveysRepository.GetSurveys(Session.GetCurrentUser()).Single(s => s.Id == sourceId);
            survey.Id = 0;
            survey.Title = survey.Title + "_Copy";
            var id = _surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
            return View("AddSurvey", id);
        }

        public ActionResult AddSurvey(ulong sourceId)
        {
            var res = _surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

            if (res == null)
            {
                return RedirectToAction("Index");
            }
            return View(res);
        }

        [HttpPost]
        public ActionResult AddSurvey(Survey survey)
        {
            return View();
        }

        public ActionResult Details(ulong sourceId)
        {
            var res = _surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

            if (res == null)
            {
                return RedirectToAction("Index");
            }
            return View(res);
        }

        [HttpPost]
        public ActionResult Details(Survey editedSurvey)
        {
            _surveysRepository.AddSurvey(Session.GetCurrentUser(), editedSurvey);
            return View();
        }

        public ActionResult EditSurvey(int? surveyTemplateId)
        {
            var editSurveyViewModel = _surveyService.GetEditSurveyViewModel(surveyTemplateId);

            return View(editSurveyViewModel);
        }

        [HttpPost]
        public ActionResult EditSurvey(EditSurveyViewModel editSurveyViewModel)
        {
            SurveyTemplate surveyTemplate;

            if (editSurveyViewModel.Id == null)
            {
                surveyTemplate = _surveyService.CreateSurveyTemplate(editSurveyViewModel);
            }
            else
            {
                surveyTemplate = _surveyService.EditSurveyTemplate(editSurveyViewModel);
            }

            return RedirectToAction("EditSurvey", new {surveyTemplateId = surveyTemplate.Id});
        }

        public ActionResult EditRespondent(QuestionAction? questionAction)
        {
            var editRespondentViewModel = _surveyService.GetEditRespondentViewModel(questionAction);

            return View(editRespondentViewModel);
        }

        public ActionResult EditQuestion(EditQuestionViewModel editQuestionViewModel)
        {
            _surveyService.CreateQuestion(editQuestionViewModel);

            throw new NotImplementedException();
        }

        public ActionResult Relationships()
        {
            return View();
        }

        public ActionResult PreviewSurvey()
        {
            return View();
        }
    }
}
