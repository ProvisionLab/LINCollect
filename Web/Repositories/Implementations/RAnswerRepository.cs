using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class RAnswerRepository : BaseRepository<RAnswer>, IRAnswerRepository
    {
        public RAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<RAnswer>> GetByQuestion(int questionId)
        {
            return Task.FromResult(DbSet.Where(x => x.RQuestionId == questionId));
        }
    }
}