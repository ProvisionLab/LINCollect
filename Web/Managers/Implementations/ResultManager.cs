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
    public class ResultManager:CrudManager<Result, ResultModel>, IResultManager
    {
        public ResultManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.ResultRepository, objectMapper)
        {
        }

        public async Task<int> InsertSection(ResultSectionModel item)
        {
            var model = Mapper.Map<ResultSectionModel, ResultSection>(item);
            await UnitOfWork.ResultSectionRepository.Insert(model);
            await UnitOfWork.SaveAsync();
            return model.Id;
        }
    }
}