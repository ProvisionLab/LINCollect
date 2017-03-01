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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _objectMapper;

        public BaseManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
        {
            _unitOfWork = unitOfWork;
            _objectMapper = objectMapper;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}