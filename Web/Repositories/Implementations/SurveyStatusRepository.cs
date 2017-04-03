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
    public class SurveyStatusRepository : BaseRepository<SurveyStatus>, ISurveyStatusRepository
    {
        public SurveyStatusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SurveyStatus> GetByName(string name)
        {
            return Task.FromResult(DbSet.FirstOrDefault(t => t.Name == name));
        }
    }
}