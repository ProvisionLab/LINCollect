using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Data.Interfaces;
using Web.Managers.Base.Interfaces;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Base.Implementations
{
    public class CrudManager<TEntity, TModel> : BaseCrudManager<TEntity>, ICrudManager<TModel>
         where TModel : class, IModel
        where TEntity : class, IEntity

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        private readonly IObjectMapper _objectMapper;

        public CrudManager(IUnitOfWork unitOfWork, IRepository<TEntity> repository, IObjectMapper objectMapper) : base(unitOfWork, repository, objectMapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _objectMapper = objectMapper;
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            await _repository.Delete(_objectMapper.Map<TModel, TEntity>(model));
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<List<TModel>> GetAsync()
        {
            return _objectMapper.Map<IEnumerable<TEntity>, List<TModel>>(await _repository.GetAll());
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            return _objectMapper.Map<TEntity, TModel>(await _repository.Get(id));
        }

        public virtual async Task<int> InsertAsync(TModel model)
        {
            var dbModel = _objectMapper.Map<TModel, TEntity>(model);
            await _repository.Insert(dbModel);
            await _unitOfWork.SaveAsync();
            return dbModel.Id;
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            await _repository.Update(_objectMapper.Map<TModel, TEntity>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}