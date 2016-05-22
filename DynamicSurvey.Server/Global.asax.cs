using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using DynamicSurvey.Core;
using DynamicSurvey.Core.SessionStorage;
using DynamicSurvey.Server.Infrastructure;

namespace DynamicSurvey.Server
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            PersistenceContext.SetConnectionString(SurveysConfig.ConnectionString);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest()
        {
            PersistenceContext.DisposeCurrentSession();
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}
