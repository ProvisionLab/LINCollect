using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyPageMap : ClassMap<SurveyPage>
    {
        public SurveyPageMap()
        {
            Id(it => it.Id);
            Map(it => it.PageIndex).Not.Nullable();
            Map(it => it.PageTitle).Not.Nullable();
            References(it => it.SurveyTemplate).Not.Nullable().Column("SurveyTemplate_Id");
        }
    }
}
