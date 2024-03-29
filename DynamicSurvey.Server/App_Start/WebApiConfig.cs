﻿using DynamicSurvey.Server.Infrastructure.ApiSession;
using System.Web.Http;
using System.Web.Routing;

namespace DynamicSurvey.Server
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
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
			


			// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
			// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
			// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
			//config.EnableQuerySupport();

			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			config.EnableSystemDiagnosticsTracing();
		}
	}
}
