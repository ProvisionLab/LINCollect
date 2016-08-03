using System.Security;
using System.Web.Mvc;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Models;

namespace DynamicSurvey.Server.Controllers
{
    public class ProfileController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            AdminAuth.IsAutorizedFlag = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string returnUrl)
        {
            if ( Request["username"] == "LINCollectAdmin" && Request["password"] == "collect2016")
            {
                AdminAuth.IsAutorizedFlag = true;
                return Redirect("/Surveys/EditSurvey");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AdminAuth.IsAutorizedFlag = false;
            return RedirectToAction("Login", "Action");
        }

        //private readonly IUsersRepository _usersRepository;

        //public ProfileController(IUsersRepository usersRepository)
        //{
        //    _usersRepository = usersRepository;
        //}

        //public ActionResult Index()
        //{
        //    // get current profile from session
        //    // and return profile page
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(User user)
        //{
            //if (!_usersRepository.Authorize(user.Username, user.Password))
            //{
            //    ModelState.AddModelError("Invalid email or password", new SecurityException());
            //    return View();
            //}

            //user = _usersRepository.GetUserByName(user.Username);

            //Session.SetCurrentUser(user);

            //// fetch all user fields via database
            //// if not valid - return view;

            //if (user.AccessRight.AccessLevel == AccessLevel.Administrator)
            //{
            //    return RedirectToAction("Index", "Surveys");
            //}
            //return RedirectToAction("Index");
        //}
    }
}
