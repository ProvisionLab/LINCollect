using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Models;
using DynamicSurvey.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.DAL.Repositories;

namespace DynamicSurvey.Server.Controllers
{
	public class SurveysController : Controller
	{
		private readonly ISurveysRepository surveysRepository;

		public SurveysController(ISurveysRepository surveysRepository)
		{
			this.surveysRepository = surveysRepository;
		}

		public ActionResult FreeFakes()
		{
			var res = surveysRepository.GetSurveys(null);
			return Json(res, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Index(
			ReturnFormat returnFormat = ReturnFormat.Html)
		{

			var res = surveysRepository.GetSurveys(null);
			if (returnFormat == ReturnFormat.Json)
			{
				return Json(res, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return View(new SurveysViewModel()
					{
						Surveys = res
					}
					);
			}
		}

		public ActionResult CopySurvey(int sourceId)
		{
			var survey = surveysRepository.GetSurveys(null).Single(s => s.Id == sourceId);
			survey.Id = 0;
			survey.Title = survey.Title + "_Copy";
			var id = surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
			return View("AddSurvey", id);
		}

		public ActionResult AddSurvey(int sourceId)
		{
			var res = surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

			if (res == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(res);
			}
		}


		[HttpPost]
		public ActionResult AddSurvey(Survey survey)
		{
			return View();
		}

		public ActionResult Details(int sourceId)
		{
			var res = surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

			if (res == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(res);
			}
		}

		[HttpPost]
		public ActionResult Details(Survey editedSurvey)
		{
			surveysRepository.AddSurvey(Session.GetCurrentUser(), editedSurvey);
			return View();
		}

	}
}