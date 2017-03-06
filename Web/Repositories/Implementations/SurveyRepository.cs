using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class SurveyRepository: BaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task Publish(int id)
        {
            var item = await Get(id);

            item.Status = DbContext.SurveyStatuses.FirstOrDefault(s => s.Name == "Published");

            await this.Update(item);
        }
    }
}