using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.ViewModels;
using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Server.Infrastructure.Authentication;

namespace DynamicSurvey.Server.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveysRepository _surveysRepository;

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository;
        }

        [CustomAuthentication]
        public ActionResult Index()
        {

			var res = _surveysRepository.GetSurveys(SessionHelper.User);
            return View(new SurveysViewModel
            {
                Surveys = res
            });
        }

        [CustomAuthentication]
        public ActionResult CopySurvey(ulong sourceId)
        {
			var survey = _surveysRepository.GetSurveys(SessionHelper.User)
                .Single(s => s.Id == sourceId);
            survey.Id = 0;
            survey.Title = survey.Title + "_Copy";
            _surveysRepository.AddSurvey(SessionHelper.User, survey);
            return View("AddSurvey");
        }

        [CustomAuthentication]
        public ActionResult AddSurvey(ulong sourceId)
        {
            var res = _surveysRepository.GetSurveyById(SessionHelper.User,sourceId);

            if (res == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [CustomAuthentication]
        public ActionResult AddSurvey(Survey survey)
        {
            return View();
        }

        [CustomAuthentication]
        public ActionResult Details(ulong sourceId)
        {
            var res = _surveysRepository.GetSurveyById(SessionHelper.User, sourceId);

            if (res == null)
            {
                return RedirectToAction("Index");
            }
            return View(res);
        }

        [HttpPost]
        [CustomAuthentication]
        public ActionResult Details(Survey editedSurvey)
        {
            _surveysRepository.AddSurvey(SessionHelper.User, editedSurvey);
            return View();
        }
    }
}
