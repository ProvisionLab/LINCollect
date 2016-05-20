using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(it => it.Id);
            Map(it => it.Name);
            References(it => it.Country).Column("Country_Id");
        }
    }
}
