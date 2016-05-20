using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(it => it.Id);
            Map(it => it.Name).Unique();
        }
    }
}
