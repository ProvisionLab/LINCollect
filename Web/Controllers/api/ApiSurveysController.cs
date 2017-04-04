using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Linconnect.Controllers.api.Result;
using Microsoft.AspNet.Identity.Owin;
using Web.Controllers.api.Base;
using Web.Data;
using Web.Filters;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Interfaces;

namespace Web.Controllers.api
{
    [RoutePrefix("api/survey")]
    public class ApiSurveysController : ApiController
    {
        private readonly ISurveyManager _surveyManager;
        private readonly ITokenManager _tokenManager;
        private ApplicationUserManager _userManager;

        public ApiSurveysController(ISurveyManager surveyManager, ITokenManager tokenManager)
        {
            _surveyManager = surveyManager;
            _tokenManager = tokenManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }


        [HttpGet,Route("")]
        [ApiAuthorize]
        public async Task<OperationResultBase> GetSurveys()
        {
            var token = await _tokenManager.GetCurrentTokenObjectAsync();
            var user = UserManager.Users.Include(t => t.Surveys).FirstOrDefault(t => t.Id == token.UserId);

            if (user != null)
                return new DataOperationResult<IEnumerable<SurveyModel>>(
                    Mapper.Map<IEnumerable<Survey>, IEnumerable<SurveyModel>>(user.Surveys));

            return new DataOperationResult<IEnumerable<SurveyModel>>();
        }

    }
}
