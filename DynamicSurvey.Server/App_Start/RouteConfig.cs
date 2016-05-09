using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DynamicSurvey.Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Users/Page{page}",
                defaults: new
                {
                    controller = "Users",
                    action = "Index",
                    page = 1
                }
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}",
                defaults: new { action = "Index" }
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}",
                defaults: new { controller = "Profile", action = "Login"}
            );

            
        }
    }
}