using System;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.DAL;

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
        public ActionResult Search(string search = "")
        {
            throw new NotImplementedException();
        }

        // /users/username
        [HttpGet]
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
        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Create(object user)
        {
            throw new NotImplementedException();
        }
    }
}
