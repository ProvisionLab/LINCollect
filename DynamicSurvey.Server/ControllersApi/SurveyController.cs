using DynamicSurvey.Server.ControllersApi.Result;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DynamicSurvey.Server.ControllersApi
{
    public class SurveyController : ApiController
    {
		private readonly ISurveysRepository surveysRepository;
		public SurveyController()
		{
			surveysRepository = new SurveysRepository();
		}
		public OperationResultBase Get()
        {
			var user = HttpContext.Current.Session.GetCurrentUser();
			if (user == null)
			{
				return OperationResultBase.Unauthorized;
			}

			return new DataOperationResult<Survey>()
			{
				Data = surveysRepository.GetSurveys(user, true)
			};
        }

        // GET api/surveysapi/5
		public OperationResultBase Get(int id)
        {
			var user = HttpContext.Current.Session.GetCurrentUser();
			if (user == null)
			{
				return OperationResultBase.Unauthorized;
			}

			return new DataOperationResult<Survey>()
			{
				Data = new Survey[] 
				{
					surveysRepository.GetSurveyById(user, (ulong) id)
				}
			};
        }
    }
}
