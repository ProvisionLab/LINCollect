using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class NQuestionRepository : BaseRepository<NQuestion>, INQuestionRepository
    {
        public NQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}