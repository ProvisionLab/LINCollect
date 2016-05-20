namespace DynamicSurvey.Core.Entities
{
    public class Vocabulary
    {
        public virtual int Id { get; protected set; }
        public virtual string Word { get; set; }
        public virtual UserLanguage UserLanguage { get; set; }
    }
}
