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
        Task<int> SaveAsync();
    }
}