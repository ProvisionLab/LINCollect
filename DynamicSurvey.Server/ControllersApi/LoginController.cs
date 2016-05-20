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
		public OperationResultBase Put([FromBody] User user)
		{
			if (!usersRepository.Authorize(user.Username, user.Password))
			{
				ResponseMessage(new HttpResponseMessage(HttpStatusCode.Unauthorized));
				return OperationResultBase.Unauthorized;
			}

			var currentUser = usersRepository.GetUserByName(user.Username);

			HttpContext.Current.Session.SetCurrentUser(user);
			return OperationResultBase.Success;
		}

	}
}
