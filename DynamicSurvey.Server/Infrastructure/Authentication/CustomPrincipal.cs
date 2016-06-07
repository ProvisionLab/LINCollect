using System.Security.Principal;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.Infrastructure.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly User user;

        public CustomPrincipal(User user)
        {
            this.user = user;
            this.Identity = new GenericIdentity(user.Username);
        }

        public bool IsInRole(string role)
        {
            return user.AccessRight.Name.Equals(role);
        }

        public IIdentity Identity { get; private set; }
    }
}