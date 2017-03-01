using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

            public DateTime LastActivity { get; set; }
        }

        private static CurrentUserService _instance;

        //One hour
        private int timeout = 3600000;

        private List<CachedUser> _users;

        private CurrentUserService()
        {
            this._users = new List<CachedUser>();
            InitInactiveClear();
        }

        public static CurrentUserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CurrentUserService();
                }
                return _instance;
            }
        }

        public async Task<ApplicationUser> GetCurrent(bool force = false)
        {
            var token = this.GetToken();
            var cached = this.GetCached(token);
            if (cached == null)
            {
                //var user = await this.GetFromDatabase(token);
//                CacheUser(user, token);
            }

            return null;
        }

        private void CacheUser(ApplicationUser user, string token)
        {
            _users.Add(new CachedUser {LastActivity = DateTime.UtcNow, Token = token, User = user});
        }

        //private Task<ApplicationUser> GetFromDatabase(string token)
        //{
        //    var userId = _tokenManaer.GetUserId(token);
        //    var user = 
        //}

        public string GetToken()
        {
            //TODO: Implement token
            return "";
        }

        private CachedUser GetCached(string token)
        {
            var cached = _users.FirstOrDefault(u => u.Token == token);
            if (cached != null)
            {
                cached.LastActivity = DateTime.UtcNow;
            }
            return cached;
        }

        private void InitInactiveClear()
        {
            var timer = new Timer(this.timeout);
            timer.Elapsed += (sender, args) =>
            {
                var lastTimeout = DateTime.UtcNow.AddMilliseconds(this.timeout);
                this._users.RemoveAll(u => u.LastActivity < lastTimeout);
            };
            timer.Start();
        }
    }
}
