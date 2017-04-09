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
    public class RQuestionManager: CrudManager<RQuestion, RQuestionModel>, IRQuestionManager
    {
        public RQuestionManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper) : base(unitOfWork, unitOfWork.RQuestionRepository, objectMapper)
        {
        }

        public override async Task<int> InsertAsync(RQuestionModel item)
        {
            item.CreateDateUtc = item.UpdateDateUtc = DateTime.UtcNow;

            var questionId = await base.InsertAsync(item);

            if (item.Answers?.Count > 0)
            {
                foreach (var answer in item.Answers)
                {
                    answer.RQuestionId = questionId;
                    var entity = ObjectMapper.Map<RAnswerModel, RAnswer>(answer);
                    await UnitOfWork.RAnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }
            return questionId;
        }

        public override async Task UpdateAsync(RQuestionModel model)
        {
            var answers = await UnitOfWork.RAnswerRepository.GetByQuestion(model.Id);

            foreach (var answer in answers)
            {
                await UnitOfWork.RAnswerRepository.Delete(answer);
            }

            await UnitOfWork.SaveAsync();

            if (model.Answers?.Count > 0)
            {
                foreach (var answer in model.Answers)
                {
                    answer.RQuestionId = model.Id;
                    var entity = ObjectMapper.Map<RAnswerModel, RAnswer>(answer);
                    await UnitOfWork.RAnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }

            model.CreateDateUtc = model.UpdateDateUtc = DateTime.UtcNow;
            await base.UpdateAsync(model);
        }
    }
}