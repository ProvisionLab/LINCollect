using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DynamicSurvey.Server
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            DependencyResolver.SetResolver(new  NinjectDependencyResolver());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var repository = new UsersRepository();
            repository.Init("Admin", "Password", "Administrator");
            repository.Init("Administrator", "Password", "Administrator");
            repository.Init("Enumerator", "Password", "Enumerator");
            repository.Init("Respondent", "Password", "Respondent");
        }
    }
}