using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            object filters = null, 
            SurveysSelector selector = SurveysSelector.All,  
            ReturnFormat returnFormat = ReturnFormat.Json )
        {

            var res = surveysRepository.GetSurveys(null);
            if (returnFormat == ReturnFormat.Json)
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new NotImplementedException();
            }
            //return View();
        }
    }
}