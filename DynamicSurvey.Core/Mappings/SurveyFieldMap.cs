using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldMap : ClassMap<SurveyField>
    {
        public SurveyFieldMap()
        {
            Id(it => it.Id);
            Map(it => it.FieldIndex).Not.Nullable();
            Map(it => it.Label).Not.Nullable();
            References(it => it.SurveyFieldType).Column("SurveyFieldType_Id");
            References(it => it.Group).Column("Group_Id");
            References(it => it.ParentPage).Not.Nullable().Column("ParentPage_Id");
        }
    }
}
