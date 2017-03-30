using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;

namespace Web.Managers.Interfaces
{
    public interface IResultManager: ICrudManager<ResultModel>
    {
        Task<int> InsertSection(ResultSectionModel item);
    }
}
