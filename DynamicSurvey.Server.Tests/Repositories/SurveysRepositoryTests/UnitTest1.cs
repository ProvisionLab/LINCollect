using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.DAL.Fakes;

namespace DynamicSurvey.Server.Tests.Repositories.SurveysRepositoryTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var survey = FakeSurveysFactory.CreateEnglishSurvey();
			var admin = CommonHelpers.CreateAdmin();
			ISurveysRepository repo = new SurveysRepository();
			repo.AddSurvey(admin, survey);
		}
	}
}
