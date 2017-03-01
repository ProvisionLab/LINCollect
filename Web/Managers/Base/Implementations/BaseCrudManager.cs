using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Managers.Base.Interfaces;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Base.Implementations
{
    public class BaseCrudManager<TEntity>: BaseManager, IDisposable where TEntity: class
    {
        private readonly IRepository<TEntity> _repository;

        public IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        public BaseCrudManager(IUnitOfWork unitOfWork, IRepository<TEntity>  repository, IObjectMapper objectMapper) : base(unitOfWork, objectMapper)
        {
            _repository = repository;
        }
    }
}