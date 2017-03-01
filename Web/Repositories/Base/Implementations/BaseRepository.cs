using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Base.Implementations
{
    public class BaseRepository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use in this repository", "dbContext");
            }
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public virtual Task Insert(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
                {
                    DbEntityEntry entityEntry = _dbContext.Entry(entity);
                    if (entityEntry.State != EntityState.Detached)
                    {
                        entityEntry.State = EntityState.Detached;
                    }
                    else
                    {
                        _dbSet.Add(entity);
                    }
            });
        }

        public virtual Task Update(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entityEntry = _dbContext.Entry(entity);
                if (entityEntry.State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }

                entityEntry.State = EntityState.Modified;

            });
        }

        public virtual Task Delete(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entityEntry = _dbContext.Entry(entity);

                if (entityEntry.State != EntityState.Deleted)
                {
                    _dbSet.Remove(entity);
                }
            });
        }

        public virtual async Task Delete(int id)
        {
            TEntity entity = await Get(id);
            if (entity != null)
            {
                await Delete(entity);
            }
        }

        public virtual Task<TEntity> Get(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult(_dbSet.ToList().AsEnumerable());
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}