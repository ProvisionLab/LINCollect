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
    public class ResultSectionRepository:BaseRepository<ResultSection>, IResultSectionRepository
    {
        public ResultSectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<ResultSection>> GetByResult(int resultId)
        {
            return Task.FromResult(DbSet.Where(i => i.ResultId == resultId).Include(i=>i.SectionType));
        }
    }
}