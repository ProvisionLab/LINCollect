using DynamicSurvey.Server.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	public class Access_RemoveUser_Admin_Tests
	{
		[TestMethod]
		public void Admin_Can_Remove_Admin()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			var oldUser = CommonHelpers.CreateAdmin("OldUser");

            CommonHelpers.AddToDatabase(currentUser);
            CommonHelpers.AddToDatabase(oldUser);

			IUsersRepository repository = new UsersRepository();

			bool isOldUserExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(true, isOldUserExists);

			repository.Remove(currentUser, oldUser.Username);

			var isOldUserStillExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(false, isOldUserStillExists);
		}

		[TestMethod]
		public void Admin_Can_Remove_Enumerator()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			var oldUser = CommonHelpers.CreateEnumerator("OldUser");
            CommonHelpers.AddToDatabase(currentUser);
            CommonHelpers.AddToDatabase(oldUser);

			IUsersRepository repository = new UsersRepository();
			bool isOldUserExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(true, isOldUserExists);

			repository.Remove(currentUser, oldUser.Username);

			var isOldUserStillExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(false, isOldUserStillExists);
		}

		[TestMethod]
		public void Admin_Can_Remove_Respondent()
		{
			var currentUser = CommonHelpers.CreateAdmin();
			var oldUser = CommonHelpers.CreateRespondent("OldUser");
            CommonHelpers.AddToDatabase(currentUser);
            CommonHelpers.AddToDatabase(oldUser);

			IUsersRepository repository = new UsersRepository();
			bool isOldUserExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(true, isOldUserExists);

			repository.Remove(currentUser, oldUser.Username);

			var isOldUserStillExists = repository.GetUserByName(oldUser.Username) != null;

            Assert.AreEqual(false, isOldUserStillExists);
		}		
	}
}
