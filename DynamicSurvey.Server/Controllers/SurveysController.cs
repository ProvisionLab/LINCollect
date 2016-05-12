﻿using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Models;
using DynamicSurvey.Server.ViewModels;

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
            var res = _surveysRepository.GetSurveys(null);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(ReturnFormat returnFormat = ReturnFormat.Html)
        {

            var res = _surveysRepository.GetSurveys(null);
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
            var survey = _surveysRepository.GetSurveys(null).Single(s => s.Id == sourceId);
            survey.Id = 0;
            survey.Title = survey.Title + "_Copy";
            var id = _surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
            return View("AddSurvey", id);
        }

        public ActionResult AddSurvey(int sourceId)
        {
            var res = _surveysRepository.GetSurveyById(sourceId);

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
            var res = _surveysRepository.GetSurveyById(sourceId);

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
