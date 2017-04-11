using System.Linq;
using System.Threading.Tasks;
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

        public Task<IQueryable<NQuestion>> GetByRelationship(int relationshipId)
        {
            return Task.FromResult(DbSet.Where(t => t.RelationshipItemId == relationshipId));
        }
    }
}