using System;

namespace DynamicSurvey.Core.Entities
{
    public class SurveyTemplate
    {
        public virtual int Id { get; protected set; }
        public virtual string TemplateName { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual User UserCreated { get; set; }
        public virtual User UserModified { get; set; }
        public virtual UserLanguage UserLanguage { get; set; }
    }
}
