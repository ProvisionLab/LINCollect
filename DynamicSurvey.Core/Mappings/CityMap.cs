using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(it => it.Id);
            Map(it => it.Name).Not.Nullable();
            References(it => it.Country).Not.Nullable().Column("Country_Id");
        }
    }
}
