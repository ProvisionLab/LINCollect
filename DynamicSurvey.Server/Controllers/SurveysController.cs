﻿using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Models;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.DAL.Fakes;
using System;
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

			mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 1)))
				.Returns(FakeSurveysFactory.CreateSurveyWithGroups());

			mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 2)))
				.Returns(FakeSurveysFactory.CreateEnglishSurvey());

			mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 3)))
				.Returns(FakeSurveysFactory.CreateRussianSurvey());

			Func<int, bool> anyOther = i =>
			{
				var usedIndexes = new[] { 1, 2, 3 };
				return usedIndexes.All(ui => ui != i);
			};

			mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => anyOther(i))))
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

        public ActionResult CopySurvey(int sourceId)
        {
			var survey = _surveysRepository.GetSurveys(Session.GetCurrentUser()).Single(s => s.Id == sourceId);
            survey.Id = 0;
            survey.Title = survey.Title + "_Copy";
            var id = _surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
            return View("AddSurvey", id);
        }

        public ActionResult AddSurvey(int sourceId)
        {
            var res = _surveysRepository.GetSurveyById(Session.GetCurrentUser(),sourceId);

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

        public ActionResult Details(int sourceId)
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
    }
}
