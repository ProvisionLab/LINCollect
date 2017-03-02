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
        protected IRepository<TEntity> Repository { get; }

        protected BaseCrudManager(IUnitOfWork unitOfWork, IRepository<TEntity>  repository, IObjectMapper objectMapper) : base(unitOfWork, objectMapper)
        {
            Repository = repository;
        }
    }
}