using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Id(it => it.Id);
            Map(it => it.Name).Unique();
            Map(it => it.Address).Not.Nullable();
            Map(it => it.PhoneNumber).Not.Nullable();
            Map(it => it.PostalCode).Not.Nullable();
            References(it => it.City).Not.Nullable().Column("City_Id");
        }
    }
}
