using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class User
	{
		public decimal Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public Language[] SupportedLanguages { get; set; }
		public AccessRight AccessRight { get; set; }

		public User()
		{

		}
	}

	public static class UserHelper
	{
		public static User ToContract(this user user)
		{
			return new User
			{
				Id = (int)user.id,
				Username = user.login,
				Password = user.password,
				AccessRight = new AccessRight()
				{
					AccessLevel = (AccessLevel)user.user_right.access_level,
					Name = user.user_right.name
				}
			};
		}
	}
}
