using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        Task<ApplicationUser> Get(string id);
    }
}
