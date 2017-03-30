﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface IPublishSurveyRepository: IRepository<PublishSurvey>
    {
        Task<PublishSurvey> GetByGuid(Guid guid);
    }
}
