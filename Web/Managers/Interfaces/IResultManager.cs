using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;

namespace Web.Managers.Interfaces
{
    public interface IResultManager : ICrudManager<ResultModel>
    {
        Task<int> InsertSection(ResultSectionModel item);
        Task<IEnumerable<ResultModel>> GetResults(int surveyId);
        Task<IEnumerable<ResultSectionModel>> GetSections(int resultId);
        Task<IEnumerable<QuestionAnswerModel>> GetAnswers(int resultSectionId);
    }
}