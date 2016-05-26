using System;
using System.Collections.Generic;

namespace DynamicSurvey.Core.Entities
{
    public class SurveyTemplate
    {
        public virtual int Id { get; protected set; }
        public virtual string TemplateName { get; set; }
        public virtual string IntroductionText { get; set; }
        public virtual string ThankYouText { get; set; }
        public virtual string LandingPageText { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual User UserCreated { get; set; }
        public virtual User UserModified { get; set; }
        public virtual UserLanguage UserLanguage { get; set; }
        public virtual IList<SurveyPage> SurveyPages { get; set; }
    }
}
