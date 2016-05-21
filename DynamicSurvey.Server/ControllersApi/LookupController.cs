using DynamicSurvey.Server.ControllersApi.Result;
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

		public LookupController()
		{
			lookupRepository = new LookupRepository();
		}

		[HttpGet]

		public OperationResultBase Country()
		{
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
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
		public OperationResultBase City()
		{
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
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
		public OperationResultBase Company()
		{
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
				return new DataOperationResult<StringEntity>()
				{
					Data = lookupRepository.GetCompanies(),
				};
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}

		}

		[HttpGet]
		public OperationResultBase Company(ulong id)
		{
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
				return new DataOperationResult<Company>(lookupRepository.GetCompanyById(id));
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}

		}

		[HttpGet]
		public OperationResultBase CompanyPost([FromBody]Company[] values)
		{
			try
			{
				HttpContext.Current.Session.ThrowIfNotAuthorized();
				var user = HttpContext.Current.Session.GetCurrentUser();
				foreach (var value in values)
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
