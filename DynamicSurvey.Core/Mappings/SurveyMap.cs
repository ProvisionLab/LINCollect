using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyMap : ClassMap<Survey>
    {
        public SurveyMap()
        {
            Id(it => it.Id);
            References(it => it.Enumerator).Column("Enumerator_Id");
            References(it => it.Respondent).Column("Respondent_Id");
            References(it => it.Company).Column("Company_Id");
        }
    }
}
