using DynamicSurvey.Server.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	public class Access_AddUser_Enumarator_Tests
	{
		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Add_Admin()
		{
			var admin = CommonHelpers.CreateAdmin();
			var enumerator = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldAdmin = repository.GetUserByName(admin.Username);
			repository.AddOrUpdate(enumerator, admin);

			var newAdmin = repository.GetUserByName(admin.Username);

            Assert.AreEqual(null, oldAdmin);
            Assert.AreEqual(null, newAdmin);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Add_Enumerator()
		{
			var enumerator = CommonHelpers.CreateEnumerator();
			var secondEnumerator = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldEnumerator = repository.GetUserByName(enumerator.Username);

			repository.AddOrUpdate(secondEnumerator, enumerator);

			var newEnumerator = repository.GetUserByName(enumerator.Username);

            Assert.AreEqual(null, oldEnumerator);
			Assert.AreEqual(null, newEnumerator);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Add_Respondent()
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
