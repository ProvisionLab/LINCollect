using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web.Managers.Implementations
{
    public class PublishSurveyManager: CrudManager<PublishSurvey, PublishSurveyModel>, IPublishSurveyManager
    {
        public PublishSurveyManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.PublishSurveyRepository, objectMapper)
        {

        }

        public async Task<PublishSurveyModel> GetByGuidAsync(Guid guid)
        {
            return
                Mapper.Map<PublishSurvey, PublishSurveyModel>(
                    await UnitOfWork.PublishSurveyRepository.GetByGuid(guid));
        }
    }
}