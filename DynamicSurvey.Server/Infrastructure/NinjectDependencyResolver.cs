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
			mock.Setup(m => m.GetSurveys(It.IsAny<User>()))
				.Returns(Fakes.FakeSurveysFactory.CreateSurveyList());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 1)))
				.Returns(Fakes.FakeSurveysFactory.CreateSurveyWithGroups());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 2)))
				.Returns(Fakes.FakeSurveysFactory.CreateEnglishSurvey());

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => i == 3)))
				.Returns(Fakes.FakeSurveysFactory.CreateRussianSurvey());

			Func<int, bool> anyOther = (i) =>
			{
				var usedIndexes = new int[] { 1, 2, 3 };
				return usedIndexes.All(ui => ui != i);
			};

			mock.Setup(m => m.GetSurveyById(It.Is<int>(i => anyOther(i))))
							.Returns(Fakes.FakeSurveysFactory.CreateRussianSurvey());

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