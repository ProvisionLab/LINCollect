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
    public class ResultRepository: BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<Result>> GetBySurveyIdAsync(int surveyId)
        {
            return
                Task.FromResult(DbSet.Where(t => (t.PublishSurvey.SurveyId == surveyId) && (t.PublishSurvey.IsPassed)).Include(i=>i.PublishSurvey));
        }
    }
}