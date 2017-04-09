using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class RQuestionRepository : BaseRepository<RQuestion>, IRQuestionRepository
    {
        public RQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}