using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Web.Managers.Base.Implementations;
using Web.Managers.Implementations;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Base.Interfaces;
using Web.Repositories.Implementations;
using Web.Repositories.Interfaces;
using Web.Services.Implementations;
using Web.Services.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var container = InitializeIoC(config);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeMapper();

        }

        protected IContainer InitializeApiIoC()
        {
            var builder = new ContainerBuilder();
            
            return builder.Build();
        }

        protected IContainer InitializeIoC(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<ObjectMapper>().As<IObjectMapper>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleSheetsService>().As<IGoogleSheetsService>().InstancePerRequest();


            //Repositories
            builder.RegisterType<SurveyRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SurveyFileRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //Managers
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<SurveyFileManager>().As<ISurveyFileManager>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf();
            return builder.Build();
        }

        protected void InitializeMapper()
        {
            Mapper.Initialize(cfg=> cfg.AddProfiles(typeof(MvcApplication).Assembly));
        }
    }
}
