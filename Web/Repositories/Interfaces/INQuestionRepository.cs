using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface INQuestionRepository : IRepository<NQuestion>
    {
        Task<IQueryable<NQuestion>> GetByRelationship(int relationshipId);
    }
}