﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Managers.Interfaces
{
    public interface ISurveyManager
    {
        Task Publish(int id);
    }
}
