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
    public class SurveyFileManager: CrudManager<SurveyFile, SurveyFileModel>,ISurveyFileManager
    {
        public SurveyFileManager(IUnitOfWork unitOfWork, IRepository<SurveyFile> repository, IObjectMapper objectMapper) : base(unitOfWork, repository, objectMapper)
        {
        }

        public override Task<int> InsertAsync(SurveyFileModel model)
        {
            if (string.IsNullOrEmpty(model.Link) || string.IsNullOrEmpty(model.Name))
            {
                return Task.FromResult(0);
            }

            return base.InsertAsync(model);
        }
    }
}