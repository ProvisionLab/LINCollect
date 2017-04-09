using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class RelationshipManager : CrudManager<RelationshipItem, RelationshipItemModel>, IRelationshipManager
    {
        public RelationshipManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork,
            unitOfWork.RelationshipRepository, objectMapper)
        {

        }
    }
}