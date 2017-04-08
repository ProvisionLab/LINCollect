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
        public QuestionManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
            : base(unitOfWork, unitOfWork.QuestionRepository, objectMapper)
        {
        }

        public override async Task UpdateAsync(QuestionModel model)
        {
            var answers = await UnitOfWork.AnswerRepository.GetByQuestion(model.Id);

            foreach (var answer in answers)
            {
                await UnitOfWork.AnswerRepository.Delete(answer);
            }

            await UnitOfWork.SaveAsync();
            
            foreach (var answer in model.Answers)
            {
                answer.QuestionId = model.Id;
                var entity = ObjectMapper.Map<AnswerModel, Answer>(answer);
                await UnitOfWork.AnswerRepository.Insert(entity);
            }
            await UnitOfWork.SaveAsync();

            model.Answers = null;
            
            await UnitOfWork.QuestionRepository.Update(ObjectMapper.Map<QuestionModel, Question>(model));
            await UnitOfWork.SaveAsync();
        }
    }
}