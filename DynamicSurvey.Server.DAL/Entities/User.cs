using System;
namespace DynamicSurvey.Server.DAL.Entities
{
	public class User
	{
		public ulong Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public Language[] SupportedLanguages { get; set; }
		public AccessRight AccessRight { get; set; }

		public void Trim()
		{
			Func<string, string> trimmer = s => s.Trim('\"');

			Username = trimmer(Username);
			Password = trimmer(Username);
		}
	}

	//public static class UserHelper
	//{
	//	public static User ToContract(this user user)
	//	{
	//		return new User
	//		{
	//			Id = ( ulong )user.id,
	//			Username = user.login,
	//			Password = user.password,
	//			AccessRight = new AccessRight()
	//			{
	//				AccessLevel = (AccessLevel)user.user_right.access_level,
	//				Name = user.user_right.name
	//			}
	//		};
	//	}
	//}
}
