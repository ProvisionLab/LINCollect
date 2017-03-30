using System;
using System.Threading.Tasks;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Base.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISurveyRepository SurveyRepository { get; }
        ITokenRepository TokenRepository { get; }
        ISurveyFileRepository SurveyFileRepository { get; }
        IPublishSurveyRepository PublishSurveyRepository { get; }
        IResultRepository ResultRepository { get; }
        IQuestionAnswerRepository QuestionAnswerRepository { get; }
        IResultSectionRepository ResultSectionRepository { get; }
        ISectionRepository SectionRepository { get; }
        IQuestionAnswerValueRepository QuestionAnswerValueRepository { get; }
        Task<int> SaveAsync();
    }
}