using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests
{
	[TestClass]
	public class Access_AddUser_Respondent_Tests
	{
		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Respondent_Can_Not_Add_Admin()
		{
			var respondent = CommonHelpers.CreateRespondent();
			var enumerator = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldRespondent = repository.GetUserByName(respondent.Username);

			repository.AddOrUpdate(enumerator, respondent);

			var newRespondent = repository.GetUserByName(respondent.Username);

            Assert.AreEqual(null, oldRespondent);
            Assert.AreEqual(null, newRespondent);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Respondent_Can_Not_Add_Enumerator()
		{
			var respondent = CommonHelpers.CreateRespondent();
			var enumerator = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldRespondent = repository.GetUserByName(respondent.Username);

			repository.AddOrUpdate(enumerator, respondent);

			var newRespondent = repository.GetUserByName(respondent.Username);

			Assert.AreEqual(null, oldRespondent);
            Assert.AreEqual(null, newRespondent);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Respondent_Can_Not_Add_Respondent()
		{
			var respondent = CommonHelpers.CreateRespondent();
			var enumerator = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldRespondent = repository.GetUserByName(respondent.Username);

			repository.AddOrUpdate(enumerator, respondent);

			var newRespondent = repository.GetUserByName(respondent.Username);

            Assert.AreEqual(null, oldRespondent);
            Assert.AreEqual(null, newRespondent);
		}
	}
}
