using Linconnect.Controllers.api.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Controllers.api.Request;
using Web.Filters;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.DTO;

/*
    public static final String API = "http://survey.softsln.net/api/";
    public static final String API_LOGIN = API + "login/";
    public static final String API_COUNTRIES = API + "lookup/country/";
    public static final String API_CITIES = API + "lookup/city/";
    public static final String API_COMPANIES = API + "lookup/company/";
    public static final String API_SEND_SURVEY = API + "report/";
 */
namespace Web.Controllers.api
{
    [Route("api/login")]
    public class ApiLoginController : ApiController
    {
        private readonly ITokenManager _tokenManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApiLoginController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //[ApiAuthorize]
        //public IEnumerable<string> Get([FromUri] AuthorizedRequest user)
        //{
        //    var _user = UserManager.FindByEmail(user.Username);
        //    return new string[] { _user.Email, _user.Id };
        //}


        [HttpPost]
        [InvalidModelStateFilter]
        public async Task<OperationResultBase> Login(AuthorizedRequest loginData)
        {
            if (loginData == null)
            {
                return FailedOperationResult.Unauthorized;
            }
            var user = UserManager.FindByEmail(loginData.Username);

            if (user == null)
            {
                return FailedOperationResult.Unauthorized;
            }

            var result = SignInManager.PasswordSignIn(user.Email, loginData.Password, true, false);

            if (result == SignInStatus.Success)
            {
                var token = await _tokenManager.GenerateToken();
                await _tokenManager.InsertAsync(new TokenModel { Key = token, UserId = user.Id});
                return new OperationResultDynamic()
                {
                    HttpResponse = HttpStatusCode.OK,
                    Result =  new {Username = user.UserName, Token = token}
                };
            }

            return FailedOperationResult.Unauthorized;
        }
        
        [HttpPut]
        [ApiAuthorize]
        public async Task<OperationResultBase> Logout()
        {
            var token = await _tokenManager.GetCurrentTokenObjectAsync();

            await _tokenManager.DeleteAsync(token.Id);

            return new OperationResultDynamic() {HttpResponse = HttpStatusCode.OK, Result = "Sucessfully logout"};
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}