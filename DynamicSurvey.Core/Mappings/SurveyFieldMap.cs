using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyFieldMap : ClassMap<SurveyField>
    {
        public SurveyFieldMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.DisplayOrder).Column("display_order");
            Map(it => it.FieldIndex).Not.Nullable().Column("field_index");
            Map(it => it.Label).Not.Nullable().Column("label");
            References(it => it.SurveyFieldType).Column("fk_survey_field_type_id");
            References(it => it.Group).Column("fk_group_id");
            References(it => it.ParentPage).Not.Nullable().Column("fk_parent_page_id");
            HasMany(it => it.Choices).Inverse().Cascade.AllDeleteOrphan();
            HasMany(it => it.SurveyFieldVocabularyCrossList).Inverse().Cascade.AllDeleteOrphan();
            Table("survey_field");
        }
    }
}
