using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Managers.Base.Interfaces;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Interfaces
{
    public interface IPublishSurveyManager: ICrudManager<PublishSurveyModel>
    {
    }
}
