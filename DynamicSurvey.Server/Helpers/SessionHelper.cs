using System.Web;
using System.Web.SessionState;
using DynamicSurvey.Server.DAL.Entities;

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

		public static User GetCurrentUser(this HttpSessionState session)
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

		public static void SetCurrentUser(this HttpSessionState session, User user)
		{
			session[userKey] = user;
		}

		public static bool IsCurrentUserSet(this HttpSessionState session)
		{
			return session[userKey] != null;
		}

		public static void ThrowIfNotAuthorized(this HttpSessionState session)
		{
			if (!IsCurrentUserSet(session))
			{
				throw new System.Security.Authentication.AuthenticationException("Unauthorized api access");
			}
		}

	}
}