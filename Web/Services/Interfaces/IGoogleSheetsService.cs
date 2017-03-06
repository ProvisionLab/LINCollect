using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IGoogleSheetsService
    {
        List<string> GetCompanies(string fileId, ref string error);
        Task<List<string[]>>  GetEnumerators(string fileId, ref string error);
    }
}
