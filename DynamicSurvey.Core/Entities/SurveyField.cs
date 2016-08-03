using System.Collections.Generic;

namespace DynamicSurvey.Core.Entities
{
    public class SurveyField
    {
        public virtual int Id { get; protected set; }
        public virtual int DisplayOrder { get; set; }
        public virtual int FieldIndex { get; set; }
        public virtual string Label { get; set; }
        public virtual string ExtremePointLeft { get; set; }
        public virtual string ExtremePointRight { get; set; }
        public virtual decimal MinimumValue { get; set; }
        public virtual decimal MaximumValue { get; set; }
        public virtual decimal Resolution { get; set; }
        public virtual bool ShowValue { get; set; }
        public virtual string RowName { get; set; }
        public virtual string ColName { get; set; }
        public virtual SurveyPage ParentPage { get; set; }
        public virtual SurveyFieldType SurveyFieldType { get; set; }
        public virtual SurveyField Group { get; set; }
        public virtual ISet<SurveyField> Choices { get; set; }
        public virtual ISet<SurveyField> MatrixRows { get; set; }
        public virtual ISet<SurveyField> MatrixColumns { get; set; }
        public virtual ISet<SurveyFieldVocabularyCross> SurveyFieldVocabularyCrossList { get; set; }

        public SurveyField()
        {
            MatrixRows = new HashSet<SurveyField>();
            MatrixColumns = new HashSet<SurveyField>();
            Choices = new HashSet<SurveyField>();
            SurveyFieldVocabularyCrossList = new HashSet<SurveyFieldVocabularyCross>();
        }
    }
}
