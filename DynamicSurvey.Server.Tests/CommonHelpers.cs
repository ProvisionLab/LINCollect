using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.Tests
{
	static class CommonHelpers
	{
		public static void AddToDatabase(User user)
		{
			var usersRepository = new UsersRepository();

            
			usersRepository.AddOrUpdate(null, user);
		}

		public static User CreateAdmin(string username = "Admin", ulong id = 1, bool encrypted = false)
		{
			return new User()
			{
				Id = id,
				Username = username,
				Password = encrypted ? "DFD14FF9FA9464A63ADAEF65271E8C32" : "Password",
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
