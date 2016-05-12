using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	class Access_AddUser_Admin_Tests
	{
		[TestMethod]
		public void Admin_Can_Add_Enumerator()
		{
			var admin = CommonHelpers.CreateAdmin();
			var enumaretor = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();

			var oldEnumerator = repository.GetUserByName(enumaretor.Username);

			repository.AddOrUpdate(admin, enumaretor);

			var newEnumerator = repository.GetUserByName(enumaretor.Username);

			Assert.AreEqual(null, oldEnumerator);
			Assert.AreNotEqual(null, newEnumerator);
            Assert.AreEqual(AccessLevel.Enumerator, newEnumerator.AccessRight.AccessLevel);
		}

		[TestMethod]
		public void Admin_Can_Add_Respondent()
		{
			var admin = CommonHelpers.CreateAdmin();
			var respondent = CommonHelpers.CreateRespondent();

			IUsersRepository repository = new UsersRepository();

			var oldRespondent = repository.GetUserByName(respondent.Username);

			repository.AddOrUpdate(admin, respondent);

			var newRespondent = repository.GetUserByName(respondent.Username);

			Assert.AreEqual(null, oldRespondent);
            Assert.AreNotEqual(null, newRespondent);
            Assert.AreEqual(AccessLevel.Respondent, newRespondent.AccessRight.AccessLevel);
		}

		[TestMethod]
		public void Admin_Can_Add_Admin()
		{
			var admin = CommonHelpers.CreateAdmin();
			var secondAdmin = CommonHelpers.CreateAdmin();

			IUsersRepository repository = new UsersRepository();

			var oldAdmin = repository.GetUserByName(secondAdmin.Username);

			repository.AddOrUpdate(admin, secondAdmin);

			var newAdmin = repository.GetUserByName(secondAdmin.Username);

            Assert.AreEqual(null, oldAdmin);
            Assert.AreNotEqual(null, newAdmin);
            Assert.AreEqual(AccessLevel.Administrator, newAdmin.AccessRight.AccessLevel);
		}

	}
}
