using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class UserRightMap : ClassMap<UserRight>
    {
        public UserRightMap()
        {
            Id(it => it.Id);
            Map(it => it.Name).Not.Nullable().Unique();
            Map(it => it.AccessLevel).Not.Nullable().Unique();
            Map(it => it.LastModified);
        }
    }
}
