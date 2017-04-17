using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;
using Web.Models.ViewModels;

namespace Web.Managers.Interfaces
{
    public interface ISurveyManager : ICrudManager<SurveyModel>
    {
        Task Publish(int id);
        Task Assign(string userId, int surveyId);
        Task Dissociate(string userId, int surveyId);
        Task<List<SurveyModel>> GetPublished();
        Task<PreviewView> GetPreview(Guid id);
        Task<PreviewView> GetPreview(int id);
        Task<List<SurveyModel>> GetByUser(string id);
        Task<bool> Submit(int publishSurveyId, PollResultView model);
        Task Offline(int surveyId);
    }
}