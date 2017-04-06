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
    public class QuestionAnswerManager : CrudManager<QuestionAnswer, QuestionAnswerModel>, IQuestionAnswerManager
    {
        public QuestionAnswerManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.QuestionAnswerRepository, objectMapper)
        {

        }

        public override async Task<int> InsertAsync(QuestionAnswerModel item)
        {
            var model = Mapper.Map<QuestionAnswerModel, QuestionAnswer>(item);

            await UnitOfWork.QuestionAnswerRepository.Insert(model);
            await UnitOfWork.SaveAsync();
            if (item.Values != null)
            {
                foreach (var itemValue in item.Values)
                {
                    var value = new QuestionAnswerValue { QuestionAnswerId = model.Id, Value = itemValue };
                    await UnitOfWork.QuestionAnswerValueRepository.Insert(value);
                    await UnitOfWork.SaveAsync();
                }
            }
            return model.Id;
        }
    }
}