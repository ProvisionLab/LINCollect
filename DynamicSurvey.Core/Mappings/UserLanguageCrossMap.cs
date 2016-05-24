using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserLanguageCrossMap : ClassMap<UserLanguageCross>
    {
        public UserLanguageCrossMap()
        {
            Id(it => it.Id);
            References(it => it.User).Not.Nullable().Column("User_Id");
            References(it => it.UserLanguage).Not.Nullable().Column("UserLanguage_Id");
        }
    }
}
