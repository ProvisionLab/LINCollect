using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Access
{
	[TestClass]
	public class Access_RemoveUser_Enumerator_Tests
	{
		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Remove_Admin()
		{
			var currentUser = CommonHelpers.CreateEnumerator();
			var oldUser = CommonHelpers.CreateAdmin();

			IUsersRepository repository = new UsersRepository();
			repository.Remove(currentUser, oldUser.Username);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Remove_Enumerator()
		{
			var currentUser = CommonHelpers.CreateEnumerator();			
			var oldUser = CommonHelpers.CreateEnumerator();

			IUsersRepository repository = new UsersRepository();
			repository.Remove(currentUser, oldUser.Username);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Security.SecurityException))]
		public void Enumerator_Can_Not_Remove_Respondent()
		{
			var currentUser = CommonHelpers.CreateEnumerator();
			var oldUser = CommonHelpers.CreateRespondent();
			IUsersRepository repository = new UsersRepository();

			repository.Remove(currentUser, oldUser.Username);
		}		
	}
}
