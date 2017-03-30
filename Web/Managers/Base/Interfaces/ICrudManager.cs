using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Managers.Base.Interfaces
{
    public interface ICrudManager<TModel>: IDisposable where TModel: class, IModel
    {
        Task<TModel> GetAsync(int id);
        Task<List<TModel>> GetAsync();
        Task<int> InsertAsync(TModel item);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
        Task DeleteAsync(int id);
    }
}
