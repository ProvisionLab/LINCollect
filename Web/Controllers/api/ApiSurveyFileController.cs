using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Web.Controllers.api.Base;
using Web.Filters;
using Web.Managers.Interfaces;
using Web.Models.DTO;

namespace Web.Controllers.api
{
    [ApiAuthorize]
    [Route("api/file")]
    [InvalidModelStateFilter]
    public class ApiSurveyFileController: BaseApiController<SurveyFileModel>
    {
        public ApiSurveyFileController(ISurveyFileManager surveyFileManager) : base(surveyFileManager)
        {

        }
    }
}