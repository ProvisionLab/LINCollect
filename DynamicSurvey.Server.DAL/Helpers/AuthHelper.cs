using System;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.DAL.Helpers
{
	public static class AuthHelper
	{
		public static void ValidateCaller(this object context, User caller, AccessLevel accessLevel = AccessLevel.Administrator)
		{
			return;

#if DEBUG
			if (caller == null)
			{
				return;
			}
#endif

			if (caller.AccessRight.AccessLevel != accessLevel)
			{
				throw new System.Security.SecurityException(string.Format("caller not {0}", accessLevel));
			}
			if (context.GetUserByName(caller.Username) == null)
			{
				throw new System.ArgumentException("user {0} not found", caller.Username);
			}
		}

		public static User GetUserByName(this object context, string username)
		{
			//var user = context.user
			//			.Where(u => u.is_deleted != 1)
			//			.Where(u => u.login == username)
			//			.SingleOrDefault();

			//if (user == null)
			//{
			//	return null;
			//}

			//return user.ToContract();

			throw new NotImplementedException();
		}

	}
}
