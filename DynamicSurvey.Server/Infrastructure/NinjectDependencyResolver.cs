using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL.Repositories;
using Ninject;

namespace DynamicSurvey.Server.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IAnswersRepository>().To<AnswersRepository>();
            _kernel.Bind<ISurveysRepository>().To<SurveysRepository>();
            _kernel.Bind<IUsersRepository>().To<UsersRepository>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}
