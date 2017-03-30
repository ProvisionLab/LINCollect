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
    public class PublishSurveyRepository: BaseRepository<PublishSurvey>, IPublishSurveyRepository
    {
        public PublishSurveyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PublishSurvey> GetByGuid(Guid guid)
        {
            return Task.FromResult(DbSet.FirstOrDefault(i => i.Link == guid.ToString()));
        }
    }
}