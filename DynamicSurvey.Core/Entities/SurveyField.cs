using System.Collections.Generic;

namespace DynamicSurvey.Core.Entities
{
    public class SurveyField
    {
        public virtual int Id { get; protected set; }
        public virtual int DisplayOrder { get; set; }
        public virtual int FieldIndex { get; set; }
        public virtual string Label { get; set; }
        public virtual SurveyPage ParentPage { get; set; }
        public virtual SurveyFieldType SurveyFieldType { get; set; }
        public virtual SurveyField Group { get; set; }
        public virtual ISet<SurveyField> Choices { get; set; }
        public virtual ISet<SurveyFieldVocabularyCross> SurveyFieldVocabularyCrossList { get; set; }

        public SurveyField()
        {
            Choices = new HashSet<SurveyField>();
            SurveyFieldVocabularyCrossList = new HashSet<SurveyFieldVocabularyCross>();
        }
    }
}
