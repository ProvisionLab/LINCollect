using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests
{
	static class CommonHelpers
	{
		public static void AddToDatabase(User user)
		{
			var usersRepository = new UsersRepository();

            
			usersRepository.AddOrUpdate(null, user);
		}

		public static User CreateAdmin(string username = "Admin")
		{
			return new User()
			{
				Id = 0,
				Username = username,
				Password = "Password",
				AccessRight = new AccessRight()
				{
					AccessLevel = AccessLevel.Administrator,
					Name = "Administrator"
				}
			};
		}

		public static User CreateEnumerator(string username = "Enumerator")
		{
			return new User()
			{
				Id = 0,
				Username = username,
				Password = "Password",
				AccessRight = new AccessRight()
				{
					AccessLevel = AccessLevel.Enumerator,
					Name = "Enumerator"
				}
			};
		}

		public static User CreateRespondent(string username = "Respondent")
		{
			return new User()
			{
				Id = 0,
				Username = username,
				Password = "Password",
				AccessRight = new AccessRight()
				{
					AccessLevel = AccessLevel.Respondent,
					Name = "Respondent"
				}
			};
		}
	}
}
