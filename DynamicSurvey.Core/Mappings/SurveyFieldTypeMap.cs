using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldTypeMap : ClassMap<SurveyFieldType>
    {
        public SurveyFieldTypeMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.FieldType).Unique().Column("field_type");
            Table("survey_field_type");
        }
    }
}
