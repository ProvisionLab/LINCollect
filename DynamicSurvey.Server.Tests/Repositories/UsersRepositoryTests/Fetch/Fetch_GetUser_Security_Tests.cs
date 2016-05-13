using System.Linq;
using DynamicSurvey.Server.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Fetch
{
    [TestClass]
    public class Fetch_GetUser_Security_Tests
    {
        [TestMethod]
        public void Get_User_By_Name_Does_Not_Return_Password()
        {
            var username = "Admin_1";
            var password = "Password";
            var admin = CommonHelpers.CreateAdmin(username);
            admin.Password = password;

            CommonHelpers.AddToDatabase(admin);

            var repository = new UsersRepository();

            var fetchedAdmin = repository.GetUserByName(username);
            var resPassword = fetchedAdmin.Password;

            Assert.AreEqual(0, resPassword.Length);            
        }

        [TestMethod]
        public void Get_User_Does_Not_Return_Password()
        {

            var admin = CommonHelpers.CreateAdmin();
            CommonHelpers.AddToDatabase(CommonHelpers.CreateAdmin("Admin_1"));
            CommonHelpers.AddToDatabase(CommonHelpers.CreateEnumerator("Enumerator_1"));
            CommonHelpers.AddToDatabase(CommonHelpers.CreateRespondent("Respondent_1"));

            var repository = new UsersRepository();

            var resPasswords = repository.GetUsers(admin, new DAL.Entities.UsersFilter())
                .Select(u => u.Password);

            foreach (var password in resPasswords)
            {
                Assert.AreEqual(0, password.Length);
            }
        }
    }
}
