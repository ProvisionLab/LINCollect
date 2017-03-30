using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;
using IObjectMapper = Web.Managers.Base.Interfaces.IObjectMapper;

namespace Web.Managers.Implementations
{
    public class SectionManager: CrudManager<Section, SectionModel>, ISectionManager
    {
        public SectionManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.SectionRepository, objectMapper)
        {
        }

        public async Task<SectionModel> GetByNameAsync(string name)
        {
            var section = await UnitOfWork.SectionRepository.GetByNameAsync(name);

            return Mapper.Map<Section, SectionModel>(section);
        }
    }
}