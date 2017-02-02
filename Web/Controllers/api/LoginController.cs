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
using Newtonsoft.Json;
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
    public class LoginController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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
        
        public IEnumerable<string> Get([FromUri] AuthorizedRequest user)
        {
            var _user = UserManager.FindByEmail(user.Username);
            return new string[] { _user.Email, _user.Id };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public OperationResultBase Put([FromUri] AuthorizedRequest user)
        {
            var _user = UserManager.FindByEmail(user.Username);
            if (_user == null)
                return FailedOperationResult.Unauthorized;

            var result = SignInManager.PasswordSignIn(_user.Email, user.Password, true, false);
            if (result == SignInStatus.Success)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, _user.Id);
                    return new OperationResultDynamic()
                    {
                        Result = new
                        {
                            Username = _user.UserName,
                            Token = hash
                        }
                    };
                }
            } else
                return FailedOperationResult.Unauthorized;

            
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}