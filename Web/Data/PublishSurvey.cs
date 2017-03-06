using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class PublishSurvey : IEntity
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}