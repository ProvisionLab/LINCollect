using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web.Managers.Implementations
{
    public class TokenManager : CrudManager<Token, TokenModel>, ITokenManager
    {
        public TokenManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
            : base(unitOfWork, unitOfWork.TokenRepository, objectMapper)
        {
        }

        public async Task<TokenModel> GetCurrentTokenObjectAsync()
        {
            var headerString = HttpContext.Current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(headerString))
            {
                var headers = AuthenticationHeaderValue.Parse(headerString);
                if (headers?.Parameter != null)
                {
                   return ObjectMapper.Map<Token, TokenModel>(await this.UnitOfWork.TokenRepository.GetByKeyAsync(headers.Parameter));
                }
            }
            return null;
        }

        public Task<string> GenerateToken()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var guid = Guid.NewGuid().ToByteArray();
            var hashArray = time.Concat(guid).ToArray();
            var token = ToHex(SHA256.Create().ComputeHash(hashArray), false);
            return Task.FromResult(token);
        }

        public async Task<bool> ValidateToken()
        {
            var headerString = HttpContext.Current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(headerString))
            {
                var headers = AuthenticationHeaderValue.Parse(headerString);
                if (headers?.Parameter != null)
                {
                    var currentToken = await UnitOfWork.TokenRepository.GetByKeyAsync(headers.Parameter);
                    if (currentToken != null)
                        return true;
                }
            }
            return false;
        }

        private string ToHex(byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);

            for (var i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        public async Task<TokenModel> GetByUser(string userId)
        {
            return Mapper.Map<Token, TokenModel>(await UnitOfWork.TokenRepository.GetByUser(userId));
        }
    }
}