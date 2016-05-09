using DynamicSurvey.Server.DAL;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DynamicSurvey.Server.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUsersRepository usersRepository;
        public ProfileController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
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
            
            var username = (string) Session["Username"];
            var password = (string) Session["Password"];

            if (usersRepository.CheckCredentials(username, password))
            {
                return RedirectToAction("Index", "Surveys");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(User user, ReturnFormat returnFormat = ReturnFormat.Html)
        {
            if (!usersRepository.Authorize(user.Username, user.Password))
            {
                ModelState.AddModelError("Invalid email or password", new System.Security.SecurityException());
                return View();
            }

            user = usersRepository.GetUserByName(user.Username);

            if (returnFormat == ReturnFormat.Html)
            {
                Session["Username"] = user.Username;
                Session["Password"] = user.Password;
            }
            
            // fetch all user fields via database
            // if not valid - return view;

            if (user.AccessRight.AccessLevel == AccessLevel.Administrator)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
