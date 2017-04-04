using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entityEntry = _context.Entry(entity);

                if (entityEntry.State != EntityState.Deleted)
                {
                    _dbSet.Remove(entity);
                }
            });
        }

        public async Task Delete(int id)
        {
            ApplicationUser entity = await Get(id);
            if (entity != null)
            {
                await Delete(entity);
            }
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
            return Task.FromResult(_dbSet.AsEnumerable());
        }

        public Task<ApplicationUser> Get(string id)
        {
            return Task.FromResult(_dbSet.FirstOrDefault(t => t.Id == id));
        }
    }
}