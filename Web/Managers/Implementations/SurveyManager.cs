using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web.Managers.Implementations
{
    public class SurveyManager: CrudManager<Survey, SurveyModel>, ISurveyManager
    {
        public SurveyManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.SurveyRepository, objectMapper)
        {

        }
        
        public async Task Publish(int id)
        {
            await UnitOfWork.SurveyRepository.Publish(id);

            await base.UnitOfWork.SaveAsync();
        }

        public async Task Assign(string userId, int surveyId)
        {
            var survey = await UnitOfWork.SurveyRepository.Get(surveyId);
            var user = await UnitOfWork.UserRepository.Get(userId);
            survey.ApplicationUsers.Add(user);
            await Repository.Update(survey);
            await UnitOfWork.SaveAsync();
        }

        public async Task Dissociate(string userId, int surveyId)
        {
            var survey = await UnitOfWork.SurveyRepository.Get(surveyId);
            var user = await UnitOfWork.UserRepository.Get(userId);
            survey.ApplicationUsers.Remove(user);
            await Repository.Update(survey);
            await UnitOfWork.SaveAsync();
        }

        public async Task<List<SurveyModel>> GetPublished()
        {
            var status = await UnitOfWork.SurveyStatusRepository.GetByName("Published");
            return
                Mapper.Map<IEnumerable<Survey>, List<SurveyModel>>(
                    (await Repository.GetAll()).Where(t => t.SurveyStatusId == status.Id));
        }
    }
}