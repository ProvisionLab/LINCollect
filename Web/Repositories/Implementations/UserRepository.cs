using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbSet<ApplicationUser> _dbSet;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Users;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DetachAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
        
        public Task<ApplicationUser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Get(string id)
        {
            return Task.FromResult(_dbSet.FirstOrDefault(t => t.Id == id));
        }
    }
}