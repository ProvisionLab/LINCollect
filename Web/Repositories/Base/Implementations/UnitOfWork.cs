using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Base.Interfaces;
using Web.Repositories.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Base.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ISurveyFileRepository _surveyFileRepository;
        private SurveyRepository _surveyRepository;
        private ITokenRepository _tokenRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ISurveyRepository SurveyRepository
            => _surveyRepository ?? (_surveyRepository = new SurveyRepository(_context));

        public ITokenRepository TokenRepository
            => _tokenRepository ?? (_tokenRepository = new TokenRepository(_context));

        public ISurveyFileRepository SurveyFileRepository
            => _surveyFileRepository ?? (_surveyFileRepository = new SurveyFileRepository(_context));

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context?.Dispose();
                _context = null;
            }
        }
    }
}