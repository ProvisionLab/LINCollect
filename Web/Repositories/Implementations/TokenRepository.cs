using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Token> GetByKeyAsync(string key)
        {
            return Task.FromResult(DbSet.FirstOrDefault(t => t.Key == key));
        }
    }
}