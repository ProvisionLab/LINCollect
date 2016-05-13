using DynamicSurvey.Server.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicSurvey.Server.Tests.Repositories.UsersRepositoryTests.Authorize
{
    [TestClass]
    public class Authorize_Tests
    {
        [TestMethod]
        public void Can_Authorize_Valid_Creds()
        {
            var username = "Admin_1";
            var password = "Password";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(username, password);

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void Can_Not_Authorize_Invalid_Creds()
        {
            var username = "Admin_1";
            var password = "Password";
            var wrongPassword = "Wrong";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(username, wrongPassword);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Can_Not_Authorize_Valid_Password_Wrong_Casing()
        {
            var username = "Admin_1";
            var password = "Password";
            var wrongPassword = "pASSWORD";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(username, wrongPassword);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Can_Not_Authorize_Valid_Login_Wrong_Casing()
        {
            var username = "Admin_1";
            var password = "Password";
            var wrongLogin = "aDMON_1";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(wrongLogin, password);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Can_Not_Authorize_Empty_Password()
        {
            var username = "Admin_1";
            var password = "Password";
            var wrongPassword = "";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(username, wrongPassword);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Can_Not_Authorize_Empty_Login()
        {
            var username = "Admin_1";
            var password = "Password";
            var wrongLogin = "";

            var admin = CommonHelpers.CreateAdmin();
            admin.Username = username;
            admin.Password = password;
            CommonHelpers.AddToDatabase(admin);

            IUsersRepository repo = new UsersRepository();
            var res = repo.Authorize(wrongLogin, password);

            Assert.AreEqual(false, res);
        }
    }
}
