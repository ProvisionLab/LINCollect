using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class VocabularyMap : ClassMap<Vocabulary>
    {
        public VocabularyMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.Word).Unique().Column("word");
            References(it => it.UserLanguage).Not.Nullable().Column("language_id");
            Table("vocabulary");
        }
    }
}
