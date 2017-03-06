using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class SurveyManager: CrudManager<Survey, SurveyModel>,ISurveyManager
    {
        public SurveyManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.SurveyRepository, objectMapper)
        {

        }

        public async Task Publish(int id)
        {
            await UnitOfWork.SurveyRepository.Publish(id);

            await base.UnitOfWork.SaveAsync();
        }
    }
}