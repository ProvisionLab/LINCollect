using System.Security;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Helpers;

namespace DynamicSurvey.Server.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public ProfileController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //public ActionResult Index()
        //{
        //    // get current profile from session
        //    // and return profile page
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!_usersRepository.Authorize(user.Username, user.Password))
            {
                ModelState.AddModelError("Invalid email or password", new SecurityException());
                return View();
            }

            user = _usersRepository.GetUserByName(user.Username);

            Session.SetCurrentUser(user);

            // fetch all user fields via database
            // if not valid - return view;

            if (user.AccessRight.AccessLevel == AccessLevel.Administrator)
            {
                return RedirectToAction("Index", "Surveys");
            }
            return RedirectToAction("Index");
        }
    }
}
