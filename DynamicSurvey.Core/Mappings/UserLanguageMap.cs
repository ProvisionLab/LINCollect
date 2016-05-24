using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserLanguageMap : ClassMap<UserLanguage>
    {
        public UserLanguageMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.Name).Column("name");
            Table("user_language");
        }
    }
}
