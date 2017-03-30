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
    public class ResultSectionRepository:BaseRepository<ResultSection>, IResultSectionRepository
    {
        public ResultSectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}