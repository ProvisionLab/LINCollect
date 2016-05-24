using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DynamicSurvey.Server.Helpers;

namespace DynamicSurvey.Server.ControllersApi
{
    public class ReportController : ApiController
    {
		private readonly IAnswersRepository answersRepository;
		private readonly ILookupRepository lookupRepository;
		public ReportController()
		{
			answersRepository = new AnswersRepository();
			lookupRepository = new LookupRepository();
		}

		public OperationResultBase Post([FromBody] SurveyReport[] reports)
		{			
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
				var user = HttpContext.Current.Session.GetCurrentUser();
				foreach (var report in reports)
				{
					var companyId = lookupRepository.AddOrUpdateCompany(user, report.Company);
					answersRepository.AddReport(user, user.Id, companyId, DateTime.UtcNow, report.Report.Pages);
				}

				return OperationResultBase.Success;
			}
			catch(Exception ex)
			{
				return new FailedOperationResult(ex);
			}
		}
        
    }
}
