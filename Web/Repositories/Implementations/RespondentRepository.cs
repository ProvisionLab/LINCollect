using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class RespondentRepository : BaseRepository<Respondent>, IRespondentRepository
    {
        public RespondentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}