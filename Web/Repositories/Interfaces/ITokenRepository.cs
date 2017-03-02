using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface ITokenRepository : IRepository<Token>
    {
        Task<Token> GetByKey(string key);
    }
}