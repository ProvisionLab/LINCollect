using System.Collections.Generic;

namespace DynamicSurvey.Core.Entities
{
    public class SurveyField
    {
        public virtual int Id { get; protected set; }
        public virtual int FieldIndex { get; set; }
        public virtual string Label { get; set; }
        public virtual SurveyPage ParentPage { get; set; }
        public virtual SurveyFieldType SurveyFieldType { get; set; }
        public virtual SurveyField Group { get; set; }
        public virtual IList<SurveyField> Choices { get; set; }
        public virtual IList<SurveyFieldVocabularyCross> SurveyFieldVocabularyCrossList { get; set; }

        public SurveyField()
        {
            Choices = new List<SurveyField>();
        }
    }
}
