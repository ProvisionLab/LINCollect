using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;

namespace DynamicSurvey.Server.ControllersApi
{
    public class LoginController : ApiController
    {
        private readonly IUsersRepository _usersRepository;

        public LoginController()
        {
            _usersRepository = new UsersRepository();
        }

        // An error occurred when trying to create a controller of type 'LoginController'. Make sure that the controller has a parameterless public constructor
        //public LoginController(IUsersRepository _usersRepository)
        //{
        //	this._usersRepository = _usersRepository;
        //}

        // PUT api/login
        public OperationResultBase Put([FromBody] User user)
        {
            if (!_usersRepository.Authorize(user.Username, user.Password))
            {
                ResponseMessage(new HttpResponseMessage(HttpStatusCode.Unauthorized));
                return OperationResultBase.Unauthorized;
            }

            var currentUser = _usersRepository.GetUserByName(user.Username);

            HttpContext.Current.Session.SetCurrentUser(user);
            return OperationResultBase.Success;
        }

    }
}
