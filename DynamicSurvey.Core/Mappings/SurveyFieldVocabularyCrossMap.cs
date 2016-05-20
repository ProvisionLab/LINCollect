using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldVocabularyCrossMap : ClassMap<SurveyFieldVocabularyCross>
    {
        public SurveyFieldVocabularyCrossMap()
        {
            Id(it => it.Id);
            References(it => it.SurveyField).Not.Nullable().Column("SurveyField_Id");
            References(it => it.VocabularyWord).Not.Nullable().Column("VocabularyWord_Id");
        }
    }
}
