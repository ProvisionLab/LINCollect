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
using DynamicSurvey.Server.ControllersApi.Result;
using DynamicSurvey.Server.DAL;

namespace DynamicSurvey.Server.ControllersApi
{
    public class ReportController : ApiController
    {
		private readonly IAnswersRepository answersRepository;
		private readonly ILookupRepository lookupRepository;
		private readonly IUsersRepository usersRepository;
		public ReportController()
		{
			answersRepository = new AnswersRepository();
			lookupRepository = new LookupRepository();
			usersRepository = new UsersRepository();
		}

		[HttpPost]
		public OperationResultBase Post([FromUri]AuthorizedRequest token, [FromBody]SurveyReport[] reports)
		{			
			try
			{
				token.ThrowIfInvalid(usersRepository);
				var user = token.ToUserEntity();

				user = usersRepository.GetUserByName(user.Username);

				foreach (var report in reports)
				{
					var companyId = lookupRepository.AddOrUpdateCompany(user, report.Company);
					// todo: change to respondent name
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
