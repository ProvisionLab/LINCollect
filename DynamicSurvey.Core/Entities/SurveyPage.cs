namespace DynamicSurvey.Core.Entities
{
    public class SurveyPage
    {
        public virtual int Id { get; protected set; }
        public virtual int PageIndex { get; set; }
        public virtual string PageTitle { get; set; }
        public virtual SurveyTemplate SurveyTemplate { get; set; }
    }
}
