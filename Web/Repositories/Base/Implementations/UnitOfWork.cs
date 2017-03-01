using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Repositories.Base.Interfaces;
using Web.Repositories.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Base.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private SurveyRepository _surveyRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ISurveyRepository SurveyRepository
        {
            get
            {
                return _surveyRepository ?? (_surveyRepository = new SurveyRepository(_context));
            }
        }
        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}