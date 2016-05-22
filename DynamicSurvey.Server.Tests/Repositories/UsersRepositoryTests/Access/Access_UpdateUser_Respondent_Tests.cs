using DynamicSurvey.Server.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	public class Access_UpdateUser_Respondent_Tests
	{
		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_NotUpdate_Admin()
		{
			var currentUser = CommonHelpers.CreateRespondent();

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
		public void Enumerator_Can_NotUpdate_Enumerator()
		{
			var currentUser = CommonHelpers.CreateRespondent();

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
		public void Enumerator_Can_NotUpdate_Respondent()
		{
			var currentUser = CommonHelpers.CreateRespondent();
		
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
