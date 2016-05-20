using System;
using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Fakes;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Models;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using Moq;

namespace DynamicSurvey.Server.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveysRepository _surveysRepository;

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository;
        }

        public ActionResult FreeFakes()
        {
            var mock = new Mock<ISurveysRepository>();
            mock.Setup(m => m.GetSurveys(It.IsAny<User>(), It.IsAny<bool>()))
                .Returns(FakeSurveysFactory.CreateSurveyList());

            mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 1)))
                .Returns(FakeSurveysFactory.CreateSurveyWithGroups());

            mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 2)))
                .Returns(FakeSurveysFactory.CreateEnglishSurvey());

            mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 3)))
                .Returns(FakeSurveysFactory.CreateRussianSurvey());

            Func<ulong, bool> anyOther = i =>
            {
                var usedIndexes = new ulong[] {1, 2, 3};
                return usedIndexes.All(ui => ui != i);
            };

            mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => anyOther(i))))
                .Returns(FakeSurveysFactory.CreateRussianSurvey());

            var res = mock.Object.GetSurveys(null);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FreeFakesFromDatabase()
        {
            var res = _surveysRepository.GetSurveys(Session.GetCurrentUser(), true);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(ReturnFormat returnFormat = ReturnFormat.Html)
        {

            var res = _surveysRepository.GetSurveys(Session.GetCurrentUser());
            if (returnFormat == ReturnFormat.Json)
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
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

            editSurveyViewModel.Languages = new LanguageItemViewModel[]
            {
                new LanguageItemViewModel()
                {
                    Id = 1,
                    Name = "English"
                }
            };

            //using (var dbContext = new DbSurveysContext())
            //{
            //	var languages = dbContext.language.OrderBy(l => l.name)
            //		.Select(l => new LanguageItemViewModel
            //		{
            //			Id = l.id,
            //			Name = l.name
            //		})
            //		.ToList();

            //	editSurveyViewModel.Languages = languages;
            //}

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
