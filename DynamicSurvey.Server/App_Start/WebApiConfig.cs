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
                name: "DefaultApiFreeFakes",
                routeTemplate: "api/Fakes/{action}",
                defaults: new
                {
                    controller = "Fakes"
                });
        }
    }
}
