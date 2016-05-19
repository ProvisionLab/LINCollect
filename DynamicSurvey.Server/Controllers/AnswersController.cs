using DynamicSurvey.Server.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicSurvey.Server.Controllers
{
    public class AnswersController : Controller
    {
		private readonly IAnswersRepository answersRepository;
		public AnswersController(IAnswersRepository answersRepository)
		{
			this.answersRepository = answersRepository;
		}

		public ActionResult Index(ulong surveyId)
        {
			var reports = answersRepository.GetReports(new DAL.Filters.SurveyReportFilter() 
			{
 				 SurveyId = surveyId
			});
            return View(reports);
        }

    }
}
