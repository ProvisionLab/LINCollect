namespace DynamicSurvey.Core.Entities
{
    public class SurveyDetail
    {
        public virtual int Id { get; protected set; }
        public virtual string UserAnswer { get; set; }
        public virtual SurveyField SurveyField { get; set; }
        public virtual Survey ParentSurvey { get; set; }
    }
}
