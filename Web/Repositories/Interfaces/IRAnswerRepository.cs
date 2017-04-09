using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface IRAnswerRepository : IRepository<RAnswer>
    {
        Task<IQueryable<RAnswer>> GetByQuestion(int questionId);
    }
}