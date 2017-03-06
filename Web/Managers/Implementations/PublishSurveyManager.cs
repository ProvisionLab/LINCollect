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
    public class PublishSurveyManager: CrudManager<PublishSurvey, PublishSurveyModel>, IPublishSurveyManager
    {
        public PublishSurveyManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.PublishSurveyRepository, objectMapper)
        {
        }
    }
}