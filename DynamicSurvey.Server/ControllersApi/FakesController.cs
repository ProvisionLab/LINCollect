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

			Func<ulong, bool> anyOther = i =>
			{
				var usedIndexes = new ulong[] { 1, 2, 3 };
				return usedIndexes.All(ui => ui != i);
			};

			var res = mock.Object.GetSurveys(null);
			return Json(res);
		}

		[HttpGet]
		public IHttpActionResult FreeFakesFromDatabase()
		{
			var user = new User()
			{
				Username = "Admin",
				Password = "DFD14FF9FA9464A63ADAEF65271E8C32"
			};
			var res = surveysRepository.GetSurveys(user, true);
			return Json(res);
		}

	    [HttpGet]
	    public IHttpActionResult TemplateWithDynamicPages()
	    {
            var mock = new Mock<ISurveysRepository>();
            mock.Setup(m => m.GetSurveys(It.IsAny<User>(), It.IsAny<bool>()))
                .Returns(FakeSurveysFactory.CreateSurveyList());

            var res = mock.Object.GetSurveys(null);
            return Json(res);
        }

	}
}
