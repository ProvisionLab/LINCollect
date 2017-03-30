using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories.Base.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity: class 
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int id);
        Task DetachAsync(TEntity entity);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();

    }
}
