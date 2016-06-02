using DynamicSurvey.Server.ControllersApi.Result;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Entities.SimpleEntities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using System;
using System.Web;
using System.Web.Http;

namespace DynamicSurvey.Server.ControllersApi
{
	public class LookupController : ApiController
	{
		private readonly ILookupRepository lookupRepository;
		private readonly IUsersRepository usersRepository;

		public LookupController()
		{
			lookupRepository = new LookupRepository();
			usersRepository = new UsersRepository();
		}

		[HttpGet]

		public OperationResultBase Country([FromUri] AuthorizedRequest authorizationInfo)
		{
			try
			{
				authorizationInfo.ThrowIfInvalid(usersRepository);
				return new DataOperationResult<StringEntity>()
				{
					Data = lookupRepository.GetCountries(),
				};
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}
			
		}

		[HttpGet]
		public OperationResultBase City([FromUri] AuthorizedRequest authorizationInfo)
		{
			try
			{
				authorizationInfo.ThrowIfInvalid(usersRepository);
				return new DataOperationResult<StringEntity>()
				{
					Data = lookupRepository.GetCities(),
				};
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}

		}

		[HttpGet]
		public OperationResultBase Company([FromUri] AuthorizedRequest authorizationInfo, bool fullInfo = false)
		{
			try
			{
				authorizationInfo.ThrowIfInvalid(usersRepository);

				if (fullInfo)
				{
					return new DataOperationResult<Company>()
					{
						Data = lookupRepository.GetCompaniesFullInfo()
					};
				}
				else
				{
					return new DataOperationResult<StringEntity>()
					{
						Data = lookupRepository.GetCompanies()
					};
				}
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}

		}

		[HttpGet]
		public OperationResultBase Company(ulong id, [FromUri] AuthorizedRequest authorizationInfo)
		{
			try
			{
				authorizationInfo.ThrowIfInvalid(usersRepository);
				return new DataOperationResult<Company>(lookupRepository.GetCompanyById(id));
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}

		}

		[HttpPost]
		public OperationResultBase CompanyPost([FromUri]AuthorizedRequest request, [FromBody]Company[] companies)
		{
			try
			{
				request.ThrowIfInvalid(usersRepository);
				var user = request.ToUserEntity();

				foreach (var value in companies)
				{
					lookupRepository.AddOrUpdateCompany(user, value);
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
