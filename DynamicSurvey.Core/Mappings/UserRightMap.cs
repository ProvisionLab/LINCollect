using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserRightMap : ClassMap<UserRight>
    {
        public UserRightMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.Name).Not.Nullable().Unique().Column("name");
            Map(it => it.AccessLevel).Not.Nullable().Unique().Column("access_level");
            Map(it => it.LastModified).Column("last_modified");
            Table("user_right");
        }
    }
}
