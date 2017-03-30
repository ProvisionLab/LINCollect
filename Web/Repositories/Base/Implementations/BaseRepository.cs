using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data.Interfaces;
using Web.Models;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Base.Implementations
{
    public abstract class BaseRepository<TEntity>: IRepository<TEntity> where TEntity: class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use in this repository", "dbContext");
            }

            DbContext = dbContext;

            DbSet = dbContext.Set<TEntity>();
        }
        public virtual Task Insert(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
                {
                    DbEntityEntry entityEntry = DbContext.Entry(entity);
                    if (entityEntry.State != EntityState.Detached)
                    {
                        entityEntry.State = EntityState.Added;
                    }
                    else
                    {
                        DbSet.Add(entity);
                    }
            });
        }

        public virtual Task Update(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entityEntry = DbContext.Entry(entity);
                if (entityEntry.State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                entityEntry.State = EntityState.Modified;

            });
        }

        public virtual Task Delete(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entityEntry = DbContext.Entry(entity);

                if (entityEntry.State != EntityState.Deleted)
                {
                    DbSet.Remove(entity);
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
            return DbSet.FindAsync(id);
        }

        public virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult(DbSet.ToList().AsEnumerable());
        }

        public virtual Task DetachAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entry = DbContext.Entry(entity);

                entry.State = EntityState.Detached;
            });
        }
        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}