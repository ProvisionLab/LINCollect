using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Data;
using Web.Models;
using Web.Repositories.Base.Implementations;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class SectionRepository:BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Section> GetByNameAsync(string name)
        {
            return Task.FromResult(DbSet.FirstOrDefault(i => i.Name == name));
        }
    }
}