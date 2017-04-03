using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class InterviewerController : Controller
    {
        private readonly ISurveyManager _surveyManager;

        public InterviewerController(ISurveyManager surveyManager)
        {
            _surveyManager = surveyManager;
        }
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Interviewer
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var interviwer = await RoleManager.FindByNameAsync("Interviewer");
            return View(await UserManager.Users.Where(t => t.Roles.Select(r => r.RoleId).Contains(interviwer.Id)).Include(t => t.Surveys).ToListAsync());
        }


        // GET: Interviewer/Create
        public ActionResult Create()
        {
            return View();
        }
        [Route("interviewer/{id}/surveys")]
        public async Task<ActionResult> Surveys(string id)
        {
            ViewData["User"] = await UserManager.FindByIdAsync(id);
            var surveys = await _surveyManager.GetPublished();
            return View(surveys);
        }

        [HttpPost]
        public async Task<ActionResult> Assign(string id, int surveyId)
        {
            await _surveyManager.Assign(id, surveyId);
            return RedirectToAction("Surveys", new {id});
        }
        [HttpPost]
        public async Task<ActionResult> Dissociate(string id, int surveyId)
        {
            await _surveyManager.Dissociate(id, surveyId);
            return RedirectToAction("Surveys", new {id });
        }

        // POST: Interviewer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Interviewer");
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Interviewer/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ApplicationUser, EditInterviewerViewModel>(applicationUser));
        }

        // POST: Interviewer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditInterviewerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    Mapper.Map(model, user);
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.NewPassword);
                    }
                    await UserManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // POST: Interviewer/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(applicationUser);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
