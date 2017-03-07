using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.DTO;

namespace Web.Models.ViewModels
{
    public class SubmitView
    {
        public SubmitView()
        {
            Succeed = new List<PublishSurveyModel>();
        }
        public List<PublishSurveyModel> Succeed { get; set; }
    }
}