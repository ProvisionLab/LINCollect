using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Fakes;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using Moq;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DynamicSurvey.Server.ControllersApi
{
	public class FakesController : ApiController
	{
		private readonly ISurveysRepository surveysRepository;

		public FakesController()
		{
			surveysRepository = new SurveysRepository();
		}

		[HttpGet]
		public IHttpActionResult FreeFakes()
		{
			var mock = new Mock<ISurveysRepository>();
			mock.Setup(m => m.GetSurveys(It.IsAny<User>(), It.IsAny<bool>()))
				.Returns(FakeSurveysFactory.CreateSurveyList());

			mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 1)))
				.Returns(FakeSurveysFactory.CreateSurveyWithGroups());

			mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 2)))
				.Returns(FakeSurveysFactory.CreateEnglishSurvey());

			mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => i == 3)))
				.Returns(FakeSurveysFactory.CreateRussianSurvey());

			Func<ulong, bool> anyOther = i =>
			{
				var usedIndexes = new ulong[] { 1, 2, 3 };
				return usedIndexes.All(ui => ui != i);
			};

			mock.Setup(m => m.GetSurveyById(null, It.Is<ulong>(i => anyOther(i))))
				.Returns(FakeSurveysFactory.CreateRussianSurvey());

			var res = mock.Object.GetSurveys(null);
			return Json(res);
		}

		[HttpGet]
		public IHttpActionResult FreeFakesFromDatabase()
		{
			var res = surveysRepository.GetSurveys(HttpContext.Current.Session.GetCurrentUser(), true);
			return Json(res);
		}

	}
}
