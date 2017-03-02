using System.Threading.Tasks;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;

namespace Web.Managers.Interfaces
{
    public interface ITokenManager : ICrudManager<TokenModel>
    {
        Task<string> GenerateToken();
        Task<string> GetCurrentToken();
        Task<bool> ValidateToken();
    }
}