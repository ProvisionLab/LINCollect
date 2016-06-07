using System.Drawing.Printing;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using DynamicSurvey.Server.Helpers;

namespace DynamicSurvey.Server.Infrastructure.Authentication
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!SessionHelper.IsCurrentUserSet())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Profile",
                    action = "Login"
                }));
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (SessionHelper.IsCurrentUserSet())
            {
                var principal = new CustomPrincipal(SessionHelper.User);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;
            }
        }
    }
}