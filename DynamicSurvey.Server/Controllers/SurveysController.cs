using System;
using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Core.SessionStorage;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Services;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using NHibernate.Linq;
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

        public ActionResult EditSurvey()
        {
            var editSurveyViewModel = new EditSurveyViewModel();

            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var languages = session.Query<UserLanguage>()
                    .OrderBy(ul => ul.Name)
                    .Select(ul => new LanguageItemViewModel
                    {
                        Id = ul.Id,
                        Name = ul.Name
                    })
                    .ToList();

                editSurveyViewModel.Languages = languages;

                transaction.Commit();
            }

            return View(editSurveyViewModel);
        }

        [HttpPost]
        public ActionResult EditSurvey(EditSurveyViewModel editSurveyViewModel)
        {
            if (editSurveyViewModel.Id == null)
            {
                var survey = new Survey
                {
                    Title = editSurveyViewModel.Name,
                    IntroductionText = editSurveyViewModel.IntroductionText,
                    ThankYouText = editSurveyViewModel.ThankYouText,
                    LandingPageText = editSurveyViewModel.LandingPageText
                };

                _surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
            }

            throw new NotImplementedException();
        }

        public ActionResult AboutRespondent()
        {
            return View();
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
