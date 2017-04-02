using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class QuestionAnswerRepository: BaseRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public Task<IQueryable<QuestionAnswer>> GetByResultSection(int resultSectionId)
        {
            return Task.FromResult(DbSet.Where(i => i.ResultSectionId == resultSectionId).Include(i=>i.Values));
        }
    }
}