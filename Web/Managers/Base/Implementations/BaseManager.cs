using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Managers.Base.Interfaces;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Base.Implementations
{
    public class BaseManager: IDisposable
    {
        protected IUnitOfWork UnitOfWork;
        protected IObjectMapper ObjectMapper;

        protected BaseManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
        {
            UnitOfWork = unitOfWork;
            ObjectMapper = objectMapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}