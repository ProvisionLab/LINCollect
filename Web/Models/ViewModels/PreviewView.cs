using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.ViewModels
{
    public class PreviewView
    {
        public int SurveyId { get; set; }
        public Guid UserLinkId { get; set; }
        public string SurveyName { get; set; }
        public string Banner { get; set; }

        //start
        public string IntroductionText { get; set; }
        public string LandingText { get; set; }
        //finish
        public string ThanksText { get; set; }
        
        //after start & before finish
        public RespondentModel AboutYouBefore { get; set; }
        public RespondentModel AboutYouAfter { get; set; }

        //relationships
        public List<RelationshipItemModel> Items { get; set; }

    }

    public class Companies
    {
        public int RelationshipId { get; set; }
        public string RelationshipName { get; set; }
        public List<CompanyItem> Names {get;set;}
        public string Error { get; set; }
    }

    public class CompanyItem
    {
        public string Name;
        public bool Checked;
    }
}