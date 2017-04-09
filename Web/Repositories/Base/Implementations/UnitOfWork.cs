using System;
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
        private IPublishSurveyRepository _publishSurveyRepository;
        private ResultRepository _resultRepository;
        private IQuestionAnswerRepository _questionAnswerRepository;
        private IResultSectionRepository _resultSectionRepository;
        private ISectionRepository _sectionRepository;
        private IQuestionAnswerValueRepository _questionAnswerValueRepository;
        private IQuestionRepository _questionRepository;
        private IUserRepository _userRepository;
        private ISurveyStatusRepository _surveyStatusRepository;
        private IAnswerRepository _answerRepository;
        private IRQuestionRepository _rQuestionRepository;
        private IRAnswerRepository _rAnswerRepository;
        private INQuestionRepository _nQuestionRepository;
        private INAnswerRepository _nAnswerRepository;
        private IRelationshipRepository _relationshipRepository;
        private IRespondentRepository _respondentRepository;

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

        public IPublishSurveyRepository PublishSurveyRepository
            => _publishSurveyRepository ?? (_publishSurveyRepository = new PublishSurveyRepository(_context));

        public IResultRepository ResultRepository
            => _resultRepository ?? (_resultRepository = new ResultRepository(_context));

        public IResultSectionRepository ResultSectionRepository
            => _resultSectionRepository ?? (_resultSectionRepository = new ResultSectionRepository(_context));

        public IQuestionAnswerRepository QuestionAnswerRepository
            => _questionAnswerRepository ?? (_questionAnswerRepository = new QuestionAnswerRepository(_context));

        public ISectionRepository SectionRepository
            => _sectionRepository ?? (_sectionRepository = new SectionRepository(_context));

        public IQuestionAnswerValueRepository QuestionAnswerValueRepository
            => _questionAnswerValueRepository ?? (_questionAnswerValueRepository = new QuestionAnswerValueRepository(_context));

        public IQuestionRepository QuestionRepository
            => _questionRepository ?? (_questionRepository = new QuestionRepository(_context));

        public IUserRepository UserRepository
            => _userRepository ?? (_userRepository = new UserRepository(_context));

        public ISurveyStatusRepository SurveyStatusRepository
            => _surveyStatusRepository ?? (_surveyStatusRepository = new SurveyStatusRepository(_context));

        public IAnswerRepository AnswerRepository
            => _answerRepository ?? (_answerRepository = new AnswerRepository(_context));

        public IRQuestionRepository RQuestionRepository
            => _rQuestionRepository ?? (_rQuestionRepository = new RQuestionRepository(_context));

        public IRAnswerRepository RAnswerRepository
            => _rAnswerRepository ?? (_rAnswerRepository = new RAnswerRepository(_context));

        public INQuestionRepository NQuestionRepository
            => _nQuestionRepository ?? (_nQuestionRepository = new NQuestionRepository(_context));

        public INAnswerRepository NAnswerRepository
            => _nAnswerRepository ?? (_nAnswerRepository = new NAnswerRepository(_context));

        public IRelationshipRepository RelationshipRepository
            => _relationshipRepository ?? (_relationshipRepository = new RelationshipRepository(_context));

        public IRespondentRepository RespondentRepository
            => _respondentRepository ?? (_respondentRepository = new RespondentRepository(_context));
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