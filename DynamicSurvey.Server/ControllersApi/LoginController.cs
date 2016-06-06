using DynamicSurvey.Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.ControllersApi.Result;

namespace DynamicSurvey.Server.ControllersApi
{
	public class LoginController : ApiController
	{
		private readonly IUsersRepository usersRepository;

		public LoginController()
		{
			usersRepository = new UsersRepository();
		}

		// An error occurred when trying to create a controller of type 'LoginController'. Make sure that the controller has a parameterless public constructor
		//public LoginController(IUsersRepository usersRepository)
		//{
		//	this.usersRepository = usersRepository;
		//}

		// PUT api/login
		[HttpPut]
		public OperationResultBase Put([FromUri] AuthorizedRequest user)
		{
			try
			{
				if (!usersRepository.Authorize(user.Username, user.Password))
				{
					return FailedOperationResult.Unauthorized;
				}

				var currentUser = usersRepository.GetUserByName(user.Username);

				return new OperationResultDynamic()
				{
					Result = new
					{
						Username = currentUser.Username,
						Password = currentUser.Password,
						AccessLevel = currentUser.AccessRight.Name
					}
				};
			}
			catch (Exception ex)
			{
				return new FailedOperationResult(ex);
			}
		}

	}
}
