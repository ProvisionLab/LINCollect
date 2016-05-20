using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyDetailMap : ClassMap<SurveyDetail>
    {
        public SurveyDetailMap()
        {
            Id(it => it.Id);
            Map(it => it.UserAnswer).Not.Nullable().Length(1000);
            References(it => it.SurveyField).Not.Nullable().Column("SurveyField_Id");
            References(it => it.ParentSurvey).Column("ParentSurvey_Id");
        }
    }
}
