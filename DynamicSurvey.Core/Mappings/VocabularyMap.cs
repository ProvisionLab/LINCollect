using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class VocabularyMap : ClassMap<Vocabulary>
    {
        public VocabularyMap()
        {
            Id(it => it.Id);
            Map(it => it.Word).Unique();
            References(it => it.UserLanguage).Not.Nullable().Column("UserLanguage_Id");
        }
    }
}
