using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface INAnswerRepository : IRepository<NAnswer>
    {
        Task<IQueryable<NAnswer>> GetByQuestion(int questionId);
    }
}