using System.Threading.Tasks;
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

        public override async Task Delete(int id)
        {
            var question = await base.Get(id);
            var relId = question.RelationshipItemId;
            await base.Delete(id);
            await DbContext.SaveChangesAsync();
            DbContext.Database.ExecuteSqlCommand("exec SortRelationQuestions {0}", relId);
        }
    }
}