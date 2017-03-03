using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private class CachedUser
        {
            public string Token { get; set; }
            public ApplicationUser User { get; set; }
        }
        private ApplicationUserManager _userManager;
        private ApplicationUserManager UserManager => _userManager ?? (_userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());

        private static CurrentUserService _instance;

        private CurrentUserService() { }

        public static CurrentUserService Instance => _instance ?? (_instance = new CurrentUserService());

        public async Task<ApplicationUser> GetCurrent(bool force = false)
        {
            return await GetFromDatabase();
        }

        private async Task<ApplicationUser> GetFromDatabase()
        {
            var tokenManger = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<ITokenManager>();

            var token = await tokenManger.GetCurrentTokenObjectAsync();
            if (token != null)
            {
                return await UserManager.FindByIdAsync(token.UserId);
            }

            return null;
        }
    }
}
