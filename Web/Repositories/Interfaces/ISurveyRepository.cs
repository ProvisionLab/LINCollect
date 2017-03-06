﻿using System.Threading.Tasks;
using Web.Data;
using Web.Repositories.Base.Interfaces;

namespace Web.Repositories.Interfaces
{
    public interface ISurveyRepository: IRepository<Survey>
    {
        Task Publish(int id);
    }
}
