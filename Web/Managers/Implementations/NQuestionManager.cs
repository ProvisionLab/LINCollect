using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class NQuestionManager: CrudManager<NQuestion, NQuestionModel>, INQuestionManager
    {
        public NQuestionManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.NQuestionRepository, objectMapper)
        {
        }

        public override async Task<int> InsertAsync(NQuestionModel item)
        {
            item.CreateDateUtc = item.UpdateDateUtc = DateTime.UtcNow;

            var questionId = await base.InsertAsync(item);

            if (item.Answers?.Count > 0)
            {
                foreach (var answer in item.Answers)
                {
                    answer.NQuestionId = questionId;
                    var entity = ObjectMapper.Map<NAnswerModel, NAnswer>(answer);
                    await UnitOfWork.NAnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }
            return questionId;
        }

        public override async Task UpdateAsync(NQuestionModel model)
        {
            var answers = await UnitOfWork.NAnswerRepository.GetByQuestion(model.Id);

            foreach (var answer in answers)
            {
                await UnitOfWork.NAnswerRepository.Delete(answer);
            }

            await UnitOfWork.SaveAsync();

            if (model.Answers?.Count > 0)
            {
                foreach (var answer in model.Answers)
                {
                    answer.NQuestionId = model.Id;
                    var entity = ObjectMapper.Map<NAnswerModel, NAnswer>(answer);
                    await UnitOfWork.NAnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }

            model.CreateDateUtc = model.UpdateDateUtc = DateTime.UtcNow;
            await base.UpdateAsync(model);
        }
    }
}