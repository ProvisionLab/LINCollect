using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class QuestionAnswerRepository: BaseRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}