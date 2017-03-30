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
    public class ResultRepository: BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}