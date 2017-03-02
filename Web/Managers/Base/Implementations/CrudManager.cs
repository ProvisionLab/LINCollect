using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Data.Interfaces;
using Web.Managers.Base.Interfaces;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Base.Implementations
{
    public class CrudManager<TEntity, TModel> : BaseCrudManager<TEntity>, ICrudManager<TModel>
         where TModel : class, IModel
        where TEntity : class, IEntity
    {

        protected CrudManager(IUnitOfWork unitOfWork, IRepository<TEntity> repository, IObjectMapper objectMapper) : base(unitOfWork, repository, objectMapper)
        {
        }

        public virtual async Task DeleteAsync(int id)
        {
            await this.Repository.Delete(id);

            await this.UnitOfWork.SaveAsync();
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            await this.Repository.Delete(this.ObjectMapper.Map<TModel, TEntity>(model));

            await this.UnitOfWork.SaveAsync();
        }

        public virtual async Task<List<TModel>> GetAsync()
        {
            return ObjectMapper.Map<IEnumerable<TEntity>, List<TModel>>(await Repository.GetAll());
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            return ObjectMapper.Map<TEntity, TModel>(await Repository.Get(id));
        }

        public virtual async Task<int> InsertAsync(TModel model)
        {
            var dbModel = ObjectMapper.Map<TModel, TEntity>(model);

            await Repository.Insert(dbModel);

            await UnitOfWork.SaveAsync();

            return dbModel.Id;
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            await Repository.Update(ObjectMapper.Map<TModel, TEntity>(model));

            await UnitOfWork.SaveAsync();
        }
    }
}