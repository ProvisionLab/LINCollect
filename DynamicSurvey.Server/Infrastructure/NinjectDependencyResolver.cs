using DynamicSurvey.Server.DAL;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject.Parameters;
using Moq;
using DynamicSurvey.Server.DAL.Entities;


namespace DynamicSurvey.Server.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            var mock = new Mock<ISurveysRepository>();
            mock.Setup(m => m.GetSurveys(null))
                .Returns(Fakes.FakeSurveysFactory.CreateSurveyList());

            kernel.Bind<ISurveysRepository>().ToConstant(mock.Object);
            kernel.Bind<IUsersRepository>().To<UsersRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }


}