using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class PublishSurveyModel:IModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public SurveyModel Survey { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}