using System.Web;
using System.Web.Routing;

namespace DynamicSurvey.Server.Infrastructure.ApiSession
{
	public class SessionStateRouteHandler : IRouteHandler
	{
		IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
		{
			return new SessionableControllerHandler(requestContext.RouteData);
		}
	}  
}