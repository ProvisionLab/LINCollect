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

        public ActionResult Index()
        {
            return View();
        }

    }
}
