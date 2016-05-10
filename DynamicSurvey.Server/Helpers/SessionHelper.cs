using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DynamicSurvey.Server.Helpers
{
	public static class SessionHelper
	{
		private static readonly string userKey = "User";
		public static User GetCurrentUser(this HttpSessionStateBase session)
		{
			var res = session[userKey];
			if (res == null)
			{
				return null;
			}
			else
			{
				return (User)session[userKey];
			}
		}

		public static void SetCurrentUser(this HttpSessionStateBase session, User user)
		{
			session[userKey] = user;
		}

		public static bool IsCurrentUserSet(this HttpSessionStateBase session)
		{
			return session[userKey] != null;
		}
	}
}