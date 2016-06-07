using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicSurvey.Server.DAL.Repositories;

namespace DynamicSurvey.Server.ControllersApi.Result
{
	public class AuthorizedRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public void Trim()
		{
			Func<string, string> trimmer = s => s.Trim('\"');

			Username = trimmer(Username);
			Password = trimmer(Password);
		}

		public bool CheckCredentials(IUsersRepository usersRepository)
		{
			return usersRepository.CheckCredentials(Username, Password);
		}

		public void ThrowIfInvalid(IUsersRepository usersRepository)
		{
			Trim();
			if (!CheckCredentials(usersRepository))
				throw new System.Security.SecurityException("Unauthorized access for user " + Username);
		}

		public User ToUserEntity()
		{
			return new User()
			{
				Username = this.Username,
				Password = this.Password
			};
		}
	}
}