using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class QuestionAnswerValueRepository : BaseRepository<QuestionAnswerValue>, IQuestionAnswerValueRepository
    {
        public QuestionAnswerValueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}