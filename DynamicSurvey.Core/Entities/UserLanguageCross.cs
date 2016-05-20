namespace DynamicSurvey.Core.Entities
{
    public class UserLanguageCross
    {
        public virtual int Id { get; protected set; }
        public virtual User User { get; set; }
        public virtual UserLanguage UserLanguage { get; set; }
    }
}
