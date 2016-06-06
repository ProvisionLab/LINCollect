using System.Web.Http;

namespace DynamicSurvey.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}");
			config.Routes.MapHttpRoute(
				name: "DefaultApiLookupFull",
				routeTemplate: "api/Lookup/{action}/{Username}/{Password}/full",
				defaults: new
				{
					controller = "Lookup",
					fullInfo = true
				});

			config.Routes.MapHttpRoute(
				name: "DefaultApiLookup",
				routeTemplate: "api/Lookup/{action}/{Username}/{Password}/{id}",
				defaults: new
				{
					controller = "Lookup",
					id = RouteParameter.Optional
				});
			

			config.Routes.MapHttpRoute(
				name: "DefaultApiFreeFakes",
				routeTemplate: "api/Fakes/{action}",
				defaults: new
				{
					controller = "Fakes"

				});

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{Username}/{Password}/{id}",
				defaults: new 
				{
					id = RouteParameter.Optional
				});
		}
	}
}
