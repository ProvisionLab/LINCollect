using AutoMapper;
using System;
using System.Threading.Tasks;
using Web.Data;
using Web.Managers.Base.Implementations;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class QuestionManager : CrudManager<Question, QuestionModel>, IQuestionManager
    {
        public QuestionManager(IUnitOfWork unitOfWork, Base.Interfaces.IObjectMapper objectMapper)
            : base(unitOfWork, unitOfWork.QuestionRepository, objectMapper)
        {
        }

        public override async Task<int> InsertAsync(QuestionModel item)
        {
            var questionId = await base.InsertAsync(item);
            if (item.Answers?.Count > 0)
            {
                foreach (var answer in item.Answers)
                {
                    answer.QuestionId = questionId;
                    var entity = ObjectMapper.Map<AnswerModel, Answer>(answer);
                    await UnitOfWork.AnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }
            return questionId;
        }

        public override async Task UpdateAsync(QuestionModel model)
        {
            var answers = await UnitOfWork.AnswerRepository.GetByQuestion(model.Id);

            foreach (var answer in answers)
            {
                await UnitOfWork.AnswerRepository.Delete(answer);
            }

            await UnitOfWork.SaveAsync();

            if (model.Answers?.Count > 0)
            {
                foreach (var answer in model.Answers)
                {
                    answer.QuestionId = model.Id;
                    var entity = ObjectMapper.Map<AnswerModel, Answer>(answer);
                    await UnitOfWork.AnswerRepository.Insert(entity);
                }
                await UnitOfWork.SaveAsync();
            }

            model.CreateDateUtc = DateTime.UtcNow;
            await base.UpdateAsync(model);
        }
    }
}