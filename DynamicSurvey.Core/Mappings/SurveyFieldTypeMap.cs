using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldTypeMap : ClassMap<SurveyFieldType>
    {
        public SurveyFieldTypeMap()
        {
            Id(it => it.Id);
            Map(it => it.FieldType).Unique();
        }
    }
}
