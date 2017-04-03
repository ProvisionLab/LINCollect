using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;

namespace Web.Managers.Interfaces
{
    public interface ISurveyManager: ICrudManager<SurveyModel>
    {
        Task Publish(int id);
        Task Assign(string userId, int surveyId);
        Task Dissociate(string userId, int surveyId);
        Task<List<SurveyModel>> GetPublished();
    }
}
