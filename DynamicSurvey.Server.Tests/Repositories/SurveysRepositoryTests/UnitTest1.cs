using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.DAL.Fakes;
using DynamicSurvey.Server.DAL;
using System.Diagnostics;

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
