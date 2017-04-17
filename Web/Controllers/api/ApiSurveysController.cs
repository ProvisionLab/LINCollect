using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Linconnect.Controllers.api.Result;
using Microsoft.AspNet.Identity.Owin;
using Web.Data;
using Web.Filters;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Models.ViewModels;

namespace Web.Controllers.api
{
    [RoutePrefix("api/survey"), ApiAuthorize]
    public class ApiSurveysController : ApiController
    {
        private readonly ISurveyManager _surveyManager;
        private readonly ITokenManager _tokenManager;
        private readonly IPublishSurveyManager _publishSurveyManager;
        private ApplicationUserManager _userManager;

        public ApiSurveysController(ISurveyManager surveyManager, ITokenManager tokenManager, IPublishSurveyManager publishSurveyManager)
        {
            _surveyManager = surveyManager;
            _tokenManager = tokenManager;
            _publishSurveyManager = publishSurveyManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }


        [HttpGet, Route("")]
        public async Task<OperationResultBase> GetSurveys()
        {
            var token = await _tokenManager.GetCurrentTokenObjectAsync();

            var surveys = await _surveyManager.GetByUser(token.UserId);

            return new DataOperationResult<List<SurveyModel>>(surveys);
        }

        [HttpPost, Route("start")]
        public async Task<OperationResultBase> Start(PublishSurveyModel model)
        {
            if (model.SurveyId < 0 || await _surveyManager.GetAsync(model.SurveyId) == null)
            {
                return new FailedOperationResult(HttpStatusCode.BadRequest, "Survey id can't be empty");
            }
            //check if user assigned to project, and if survey is published

            model.Link = Guid.NewGuid().ToString();
            model.Succeed = true;
            var id = await _publishSurveyManager.InsertAsync(model);

            return new DataOperationResult<PublishSurveyModel>(await _publishSurveyManager.GetAsync(id));
        }

        [HttpGet, Route("pass/{id:int:min(1)}")]
        public async Task<OperationResultBase> Pass(int id)
        {
            return new DataOperationResult<PreviewView>(await _surveyManager.GetPreview(id));
        }

        [HttpPost]
        public async Task<OperationResultBase> Submit(PollResultView resultModel)
        {
            var publishSurvey = await _publishSurveyManager.GetByGuidAsync(resultModel.UserLinkId);

            if (publishSurvey == null)
            {
                return FailedOperationResult.BadRequest;
            }

            var result = await _surveyManager.Submit(publishSurvey.Id, resultModel);

            if (result)
            {
                return OperationResultBase.Ok;
            }

            return FailedOperationResult.BadRequest;
        }
    }
}