using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserLanguageMap : ClassMap<UserLanguage>
    {
        public UserLanguageMap()
        {
            Id(it => it.Id);
            Map(it => it.Name);
        }
    }
}
