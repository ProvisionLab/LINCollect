using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;

namespace Web.Managers.Interfaces
{
    public interface ISectionManager: ICrudManager<SectionModel>
    {
        Task<SectionModel> GetByNameAsync(string name);
    }
}
