using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Id(it => it.Id);
            Map(it => it.Name);
            Map(it => it.Address);
            Map(it => it.PhoneNumber);
            Map(it => it.PostalCode);
            References(it => it.City).Column("City_Id");
        }
    }
}
