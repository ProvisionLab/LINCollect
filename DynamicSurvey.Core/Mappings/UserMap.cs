using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(it => it.Id);
            Map(it => it.Login).Not.Nullable().Unique();
            Map(it => it.Password).Not.Nullable();
            Map(it => it.Salt).Not.Nullable();
            Map(it => it.IsDeleted).Not.Nullable();
            References(it => it.UserRight).Not.Nullable().Column("UserRight_Id");
        }
    }
}
