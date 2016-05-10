using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.ViewModels
{
    public class SurveysViewModel
    {
        public IEnumerable<Survey> Surveys { get; set; }
    }
}