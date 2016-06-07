using System;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Infrastructure.Authentication;
using DynamicSurvey.Server.ViewModels;

namespace DynamicSurvey.Server.Controllers
{
    // /users 
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // TODO: map
        // /users?search=loginpart
        [HttpGet]
        [CustomAuthentication]
        public ActionResult Search(string search = "")
        {
            throw new NotImplementedException();
        }

        // /users/username
        [HttpGet]
        [CustomAuthentication]
        public ActionResult Index(int page = 1)
        {
            var filter = new UsersFilter(pageIndex: page, pageSize: 3);
            var result = new UsersViewModel
            {
                Filter = filter,
                Users = _usersRepository.GetUsers((User) Session["User"], filter)
            };

            return View(result);
        }

        [HttpGet]
        [CustomAuthentication]
        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [CustomAuthentication]
        public ActionResult Create(object user)
        {
            throw new NotImplementedException();
        }
    }
}
