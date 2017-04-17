using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Web.Common;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Models.ViewModels;
using Web.Repositories.Base.Interfaces;
using Web.Services.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web.Managers.Implementations
{
    public class SurveyManager : CrudManager<Survey, SurveyModel>, ISurveyManager
    {
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IQuestionAnswerManager _questionAnswerManager;
        private readonly IResultManager _resultManager;
        private readonly ISectionManager _sectionManager;

        public SurveyManager(IUnitOfWork unitOfWork,
                            IObjectMapper objectMapper,
                            IGoogleSheetsService googleSheetsService,
                            IQuestionAnswerManager questionAnswerManager,
                            IResultManager resultManager,
                            ISectionManager sectionManager)
            : base(unitOfWork, unitOfWork.SurveyRepository, objectMapper)
        {
            _googleSheetsService = googleSheetsService;
            _questionAnswerManager = questionAnswerManager;
            _resultManager = resultManager;
            _sectionManager = sectionManager;
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


        public async Task<PreviewView> GetPreview(int id)
        {
            var publishSurvey = await UnitOfWork.PublishSurveyRepository.Get(id);

            return await GetPreview(publishSurvey);
        }

        public async Task<PreviewView> GetPreview(Guid id)
        {

            var publishSurvey =
                await UnitOfWork.PublishSurveyRepository.GetByGuid(id);

            return await GetPreview(publishSurvey);
        }

        private async Task<PreviewView> GetPreview(PublishSurvey publishSurvey)
        {
            if (publishSurvey == null || publishSurvey.IsPassed)
                return null;

            var surveyId = publishSurvey.SurveyId;

            if (surveyId < 1)
                return null;

            var survey = await UnitOfWork.SurveyRepository.Get(surveyId);
            if (survey == null)
                return null;

            var model = new PreviewView
            {
                UserLinkId = Guid.Parse(publishSurvey.Link),
                SurveyId = survey.Id,
                SurveyName = survey.Name,
                IntroductionText = survey.Introduction,
                LandingText = survey.Landing,
                ThanksText = survey.Thanks,
                Banner = survey.Banner,
                AboutYouBefore = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => !x.IsAfterSurvey)),
                AboutYouAfter = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey)),
                Items =
                    Mapper.Map<List<RelationshipItemModel>>(survey.RelationshipItems?.OrderBy(x => x.OrderId).ToList())
            };

            foreach (var item in model.Items)
            {
                var fileId = int.Parse(item.NodeList);
                var file = await UnitOfWork.SurveyFileRepository.Get(fileId);

                if (file == null)
                {
                    item.Companies = new Companies { RelationshipId = item.Id, Names = new List<CompanyItem>() };
                }
                else
                {
                    var companies = new List<CompanyItem>();
                    var error = "";

                    foreach (var companyName in _googleSheetsService.GetCompanies(file.Link, ref error))
                    {
                        companies.Add(new CompanyItem { Name = companyName, Checked = false });
                    }
                    var _companies = new Companies
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = companies,
                        Error = !string.IsNullOrEmpty(error) ? string.Format(error, file.Name, file.Link) : string.Empty
                    };
                    if (item.SortNodeList)
                        _companies.Names = _companies.Names.OrderBy(x => x.Name).ToList();

                    item.Companies = _companies;
                }
            }
            return model;
        }

        public async Task<List<SurveyModel>> GetByUser(string id)
        {
            return Mapper.Map<IQueryable<Survey>, List<SurveyModel>>(await UnitOfWork.SurveyRepository.GetByUser(id));
        }

        public async Task<bool> Submit(int publishSurveyId, PollResultView model)
        {
            var result = new ResultModel
            {
                PassDate = DateTime.Now,
                PublishSurveyId = publishSurveyId
            };

            int resultId;

            try
            {
                resultId = await _resultManager.InsertAsync(result);
            }
            catch
            {
                return false;
            }

            var sectionType = await _sectionManager.GetAsync();

            //Before
            if (model.AboutYouBefore != null && model.AboutYouBefore.QuestionAnswers != null)
            {
                int sectionId;
                try
                {
                    sectionId = await _resultManager.InsertSection(
                        new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.RespondentBefore).Id, SectionId = model.AboutYouBefore.Id });
                }
                catch
                {
                    return false;
                }

                foreach (var questionAnswerModel in model.AboutYouBefore.QuestionAnswers)
                {
                    questionAnswerModel.ResultSectionId = sectionId;
                    try
                    {
                        await _questionAnswerManager.InsertAsync(questionAnswerModel);

                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            //After
            if (model.AboutYouAfter?.QuestionAnswers != null)
            {
                int sectionId;
                try
                {
                    sectionId = await _resultManager.InsertSection(
                        new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.RespondentAfter).Id, SectionId = model.AboutYouBefore.Id });
                }
                catch
                {
                    return false;
                }

                foreach (var questionAnswerModel in model.AboutYouAfter.QuestionAnswers)
                {
                    questionAnswerModel.ResultSectionId = sectionId;
                    try
                    {
                        await _questionAnswerManager.InsertAsync(questionAnswerModel);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            if (model.Items != null && model.Items.Count > 0)
            {
                //Relationships
                foreach (var relationShip in model.Items)
                {
                    if (relationShip.QuestionAnswers != null)
                    {
                        int sectionId;
                        try
                        {
                            sectionId = await _resultManager.InsertSection(
                                new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.Relationship).Id, SectionId = relationShip.Id });
                        }
                        catch
                        {
                            return false;
                        }

                        foreach (var questionAnswerModel in relationShip.QuestionAnswers)
                        {
                            questionAnswerModel.ResultSectionId = sectionId;
                            try
                            {
                                await _questionAnswerManager.InsertAsync(questionAnswerModel);
                            }
                            catch
                            {
                                return false;
                            }
                        }
                    }
                    if (relationShip.NQuestionAnswers != null)
                    {
                        int sectionId;
                        try
                        {
                            sectionId = await _resultManager.InsertSection(
                         new ResultSectionModel { ResultId = resultId, SectionTypeId = sectionType.FirstOrDefault(t => t.Name == Constants.Node).Id, SectionId = relationShip.Id });

                        }
                        catch
                        {
                            return false;
                        }

                        foreach (var questionAnswerModel in relationShip.NQuestionAnswers)
                        {
                            questionAnswerModel.ResultSectionId = sectionId;
                            try
                            {
                                await _questionAnswerManager.InsertAsync(questionAnswerModel);

                            }
                            catch
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public async Task Offline(int surveyId)
        {
            var survey = GetAsync(surveyId);

            if(survey == null)
            {
                return;
            }

            await Clear(surveyId);

            await UnitOfWork.SurveyRepository.Offline(surveyId);

            await UnitOfWork.SaveAsync();
        }

        public async Task Clear(int surveyId)
        {
            var published = await UnitOfWork.PublishSurveyRepository.GetBySurvey(surveyId);
            foreach (var item in published)
            {
                await UnitOfWork.PublishSurveyRepository.Delete(item.Id);
            }
            await UnitOfWork.SaveAsync();
        }
    }
}