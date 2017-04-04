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

        public override async Task Insert(Token entity)
        {
            var token = await GetByUser(entity.UserId);
            if (token != null)
            {
                token.Key = entity.Key;
                await Update(token);
            }
            else
            {
                await base.Insert(entity);
            }
        }

        public Task<Token> GetByKeyAsync(string key)
        {
            return Task.FromResult(DbSet.FirstOrDefault(t => t.Key == key));
        }

        public Task<Token> GetByUser(string userId)
        {
            return Task.FromResult(DbSet.FirstOrDefault(t => t.UserId == userId));
        }
    }
}