using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.Login).Not.Nullable().Unique().Column("login");
            Map(it => it.Password).Not.Nullable().Column("password");
            Map(it => it.Salt).Not.Nullable().Column("salt");
            Map(it => it.IsDeleted).Not.Nullable().Column("is_deleted");
            References(it => it.UserRight).Not.Nullable().Column("user_right_id");
            Table("user");
        }
    }
}
