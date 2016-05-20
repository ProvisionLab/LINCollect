namespace DynamicSurvey.Core.Entities
{
    public class Survey
    {
        public virtual int Id { get; protected set; }
        public virtual User Enumerator { get; set; }
        public virtual User Respondent { get; set; }
        public virtual Company Company { get; set; }
    }
}
