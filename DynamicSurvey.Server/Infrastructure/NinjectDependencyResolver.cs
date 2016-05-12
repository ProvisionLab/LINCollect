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
using DynamicSurvey.Server.DAL.Fakes;


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
			mock.Setup(m => m.GetSurveys(It.IsAny<User>()))
				.Returns(FakeSurveysFactory.CreateSurveyList());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 1)))
				.Returns(FakeSurveysFactory.CreateSurveyWithGroups());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 2)))
				.Returns(FakeSurveysFactory.CreateEnglishSurvey());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 3)))
				.Returns(FakeSurveysFactory.CreateRussianSurvey());

			Func<int, bool> anyOther = (i) =>
			{
				var usedIndexes = new int[] { 1, 2, 3 };
				return usedIndexes.All(ui => ui != i);
			};

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => anyOther(i))))
							.Returns(FakeSurveysFactory.CreateRussianSurvey());

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