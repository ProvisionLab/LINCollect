using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Base.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ISurveyRepository SurveyRepository { get; }

        Task SaveAsync();
    }
}
