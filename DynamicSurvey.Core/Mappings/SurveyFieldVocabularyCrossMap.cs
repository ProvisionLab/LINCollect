using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldVocabularyCrossMap : ClassMap<SurveyFieldVocabularyCross>
    {
        public SurveyFieldVocabularyCrossMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.DisplayOrder).Column("display_order");
            References(it => it.SurveyField).Not.Nullable().Column("fk_survey_field_id");
            References(it => it.VocabularyWord).Not.Nullable().Column("fk_vocabulary_word_id");
            Table("survey_field_vocabulary_cross");
        }
    }
}
