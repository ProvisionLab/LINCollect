using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class NAnswerRepository : BaseRepository<NAnswer>, INAnswerRepository
    {
        public NAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public Task<IQueryable<NAnswer>> GetByQuestion(int questionId)
        {
            return Task.FromResult(DbSet.Where(x => x.NQuestionId == questionId));
        }
    }
}