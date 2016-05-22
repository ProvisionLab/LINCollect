using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL.Repositories;
using System.Linq;

namespace DynamicSurvey.Server.Tests.Repositories.AnswersRepositoryTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var answersRepo = new AnswersRepository();
			var templatesRepo = new SurveysRepository();
			var admin = CommonHelpers.CreateAdmin(encrypted: true);
			var survey = templatesRepo.GetSurveys(admin, true)[0];

			foreach (var page in survey.Pages)
			{
				foreach (var field in page.Fields)
				{

					field.UserAnswer = null;
					field.UserAnswer = field.DefaultValues.FirstOrDefault(s => !string.IsNullOrEmpty(s));
					if (field.UserAnswer == null)
						field.UserAnswer = "Answer";
				}
			}

			answersRepo.AddReport(admin, admin.Id, 1, DateTime.UtcNow, survey.Pages);
		}
	}
}
