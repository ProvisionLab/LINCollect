using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests
{
	[TestClass]
	public class Access_UpdateUser_Admin_Tests
	{
		[TestMethod]
		public void Admin_Can_Update_Admin()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			CommonHelpers.AddToDatabase(currentUser);

			var oldUser = CommonHelpers.CreateAdmin();
			oldUser.Username = "oldUser";
			CommonHelpers.AddToDatabase(oldUser);
			var newUser = CommonHelpers.CreateAdmin();
			newUser.Username = "newUser";

			IUsersRepository repository = new UsersRepository();

			bool isOldUserPresent = repository.GetUserByName(oldUser.Username) != null;

			repository.AddOrUpdate(currentUser, newUser);

			bool isNewUserPresent = repository.GetUserByName(newUser.Username) != null;

            Assert.AreEqual(true, isOldUserPresent);
            Assert.AreEqual(true, isNewUserPresent);
		}

		[TestMethod]
		public void Admin_Can_Update_Enumerator()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			CommonHelpers.AddToDatabase(currentUser);

			var oldUser = CommonHelpers.CreateEnumerator();
			oldUser.Username = "oldUser";
			CommonHelpers.AddToDatabase(oldUser);
			var newUser = CommonHelpers.CreateEnumerator();
			newUser.Username = "newUser";

			IUsersRepository repository = new UsersRepository();

			bool isOldUserPresent = repository.GetUserByName(oldUser.Username) != null;

			repository.AddOrUpdate(currentUser, newUser);

			bool isNewUserPresent = repository.GetUserByName(newUser.Username) != null;

            Assert.AreEqual(true, isOldUserPresent);
            Assert.AreEqual(true, isNewUserPresent);
		}

		[TestMethod]
		public void Admin_Can_Update_Respondent()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			CommonHelpers.AddToDatabase(currentUser);

			var oldUser = CommonHelpers.CreateRespondent();
			oldUser.Username = "oldUser";
			CommonHelpers.AddToDatabase(oldUser);
			var newUser = CommonHelpers.CreateRespondent();
			newUser.Username = "newUser";

			IUsersRepository repository = new UsersRepository();

			bool isOldUserPresent = repository.GetUserByName(oldUser.Username) != null;

			repository.AddOrUpdate(currentUser, newUser);

			bool isNewUserPresent = repository.GetUserByName(newUser.Username) != null;

            Assert.AreEqual(true, isOldUserPresent);
            Assert.AreEqual(true, isNewUserPresent);
		}
	}
}
