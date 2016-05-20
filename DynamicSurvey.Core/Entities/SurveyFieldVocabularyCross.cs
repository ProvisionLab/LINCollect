namespace DynamicSurvey.Core.Entities
{
    public class SurveyFieldVocabularyCross
    {
        public virtual int Id { get; protected set; }
        public virtual SurveyField SurveyField { get; set; }
        public virtual Vocabulary VocabularyWord { get; set; }
    }
}
