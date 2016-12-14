using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;

namespace Web.Models.ViewModels
{
    public class PreviewView
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Banner { get; set; }

        //start
        public string IntroductionText { get; set; }
        //finish
        public string ThanksText { get; set; }
        
        //after start & before finish
        public Respondent AboutYouBefore { get; set; }
        public Respondent AboutYouAfter { get; set; }

        //relationships
        public List<RelationshipItem> Items { get; set; }

        public List<Companies> Companies { get; set; }
    }

    public class Companies
    {
        public int RelationshipId { get; set; }
        public string RelationshipName { get; set; }
        public List<string> Names {get;set;}
        public string Error { get; set; }
    }
}