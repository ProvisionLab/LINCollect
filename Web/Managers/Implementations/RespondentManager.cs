using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class RespondentManager: CrudManager<Respondent,RespondentModel>, IRespondentManager
    {
        public RespondentManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.RespondentRepository, objectMapper)
        {
        }
    }
}