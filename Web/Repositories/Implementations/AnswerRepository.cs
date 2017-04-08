using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class AnswerRepository: BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<Answer>> GetByQuestion(int questionId)
        {
            return Task.FromResult(DbSet.Where(x => x.QuestionId == questionId));
        }
    }
}