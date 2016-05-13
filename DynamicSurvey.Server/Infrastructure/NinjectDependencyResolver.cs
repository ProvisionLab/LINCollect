using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Fakes;
using Moq;
using Ninject;
using DynamicSurvey.Server.DAL.Repositories;

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
            var mock = new Mock<ISurveysRepository>();
            mock.Setup(m => m.GetSurveys(It.IsAny<User>()))
                .Returns(FakeSurveysFactory.CreateSurveyList());

            mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 1)))
                .Returns(FakeSurveysFactory.CreateSurveyWithGroups());

            mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 2)))
                .Returns(FakeSurveysFactory.CreateEnglishSurvey());

            mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => i == 3)))
                .Returns(FakeSurveysFactory.CreateRussianSurvey());

            Func<int, bool> anyOther = i =>
            {
                var usedIndexes = new[] {1, 2, 3};
                return usedIndexes.All(ui => ui != i);
            };

            mock.Setup(m => m.GetSurveyById(null, It.Is<int>(i => anyOther(i))))
                .Returns(FakeSurveysFactory.CreateRussianSurvey());

            _kernel.Bind<ISurveysRepository>().ToConstant(mock.Object);
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
