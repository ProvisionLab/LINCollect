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
using Web.Providers;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Base.Interfaces;
using Web.Repositories.Implementations;
using Web.Services.Implementations;
using Web.Services.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var container = InitializeIoC();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeMapper();
            ServiceProvider.SetContainer(container);
        }

        protected IContainer InitializeIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<ApplicationDbContext>().AsSelf();

            //Services
            builder.RegisterType<Services.Implementations.EmailService>().As<IEmailService>().SingleInstance();

            builder.RegisterType<ObjectMapper>().As<IObjectMapper>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleSheetsService>().As<IGoogleSheetsService>().InstancePerDependency();

            //Repositories
            builder.RegisterType<SurveyRepository>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<SurveyFileRepository>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<TokenRepository>().AsImplementedInterfaces().InstancePerDependency();

            //Managers
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<SurveyFileManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<TokenManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<SurveyManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<PublishSurveyManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<ResultManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<QuestionAnswerManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<SectionManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<UserManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<QuestionManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<RQuestionManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<NQuestionManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<RelationshipManager>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<RespondentManager>().AsImplementedInterfaces().InstancePerDependency();

            return builder.Build();
        }

        protected void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(MvcApplication).Assembly));
        }
    }
}