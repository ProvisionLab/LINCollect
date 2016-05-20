namespace DynamicSurvey.Core.Entities
{
    public class SurveyFieldType
    {
        public virtual int Id { get; protected set; }
        public virtual FieldType FieldType { get; set; }
    }
}
