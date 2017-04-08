using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class PublishSurveyRepository : BaseRepository<PublishSurvey>, IPublishSurveyRepository
    {
        public PublishSurveyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<PublishSurvey> Get(int id)
        {
            return Task.FromResult(DbSet.Include(t => t.Survey).FirstOrDefault(t => t.Id == id));
        }

        public Task<PublishSurvey> GetByGuid(Guid guid)
        {
            return Task.FromResult(DbSet.FirstOrDefault(i => i.Link == guid.ToString()));
        }
    }
}