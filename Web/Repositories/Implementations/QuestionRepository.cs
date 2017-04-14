using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task Delete(int id)
        {
            var question = await base.Get(id);
            var respId = question.RespondentId;
            await base.Delete(id);
            await DbContext.SaveChangesAsync();
            DbContext.Database.ExecuteSqlCommand("exec SortRespondentQuestions {0}", respId);
        }
    }
}