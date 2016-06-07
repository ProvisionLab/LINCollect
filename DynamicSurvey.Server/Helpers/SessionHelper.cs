using System.Web;
using DynamicSurvey.Server.DAL.Entities;
using System.Web.SessionState;

namespace DynamicSurvey.Server.Helpers
{
	public static class SessionHelper
	{
		private static readonly string userKey = "User";

	    public static User User
	    {
	        get
	        {
                var session = HttpContext.Current.Session;
                var res = session[userKey];
                if (res == null)
                {
                    return new User();
                }
                else
                {
                    return (User)session[userKey];
                }
            }
	        set
	        {
                var session = HttpContext.Current.Session;
                session[userKey] = value;
            }
	    }
        
		public static bool IsCurrentUserSet()
		{
            var session = HttpContext.Current.Session;
		    var user = session[userKey];
            return user != null 
                && user is User
                && !string.IsNullOrEmpty(((User)user).Username) ;
		}
	}
}