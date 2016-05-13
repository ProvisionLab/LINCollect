using DynamicSurvey.Server.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	public class Access_UpdateUser_Enumerator_Tests
	{
		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Update_Admin()
		{
			var currentUser = CommonHelpers.CreateEnumerator();

			var oldUser = CommonHelpers.CreateAdmin();
			oldUser.Username = "oldUser";
			oldUser.Id = 2;
			var newUser = CommonHelpers.CreateAdmin();
			newUser.Username = "newUser";
			newUser.Id = 2;

			IUsersRepository repository = new UsersRepository();
			repository.AddOrUpdate(currentUser, newUser);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Update_Enumerator()
		{
			var currentUser = CommonHelpers.CreateEnumerator();

			var oldUser = CommonHelpers.CreateAdmin();
			oldUser.Username = "oldUser";
			oldUser.Id = 2;
			var newUser = CommonHelpers.CreateAdmin();
			newUser.Username = "newUser";
			newUser.Id = 2;

			IUsersRepository repository = new UsersRepository();
			repository.AddOrUpdate(currentUser, newUser);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Update_Respondent()
		{
			var currentUser = CommonHelpers.CreateEnumerator();

			var oldUser = CommonHelpers.CreateAdmin();
			oldUser.Username = "oldUser";
			oldUser.Id = 2;
			var newUser = CommonHelpers.CreateAdmin();
			newUser.Username = "newUser";
			newUser.Id = 2;

			IUsersRepository repository = new UsersRepository();
			repository.AddOrUpdate(currentUser, newUser);
		}
	}
}
