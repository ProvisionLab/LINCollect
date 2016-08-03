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
            Map(it => it.ExtremePointLeft).Column("extreme_left");
            Map(it => it.ExtremePointRight).Column("extreme_right");
            Map(it => it.MaximumValue).Column("max_value");
            Map(it => it.MinimumValue).Column("min_value");
            Map(it => it.Resolution).Column("resolution");
            Map(it => it.ShowValue).Column("show_value");
            Map(it => it.RowName).Column("row_name");
            Map(it => it.ColName).Column("col_name");
            References(it => it.SurveyFieldType).Column("fk_survey_field_type_id");
            References(it => it.Group).Column("fk_group_id");
            References(it => it.ParentPage).Not.Nullable().Column("fk_parent_page_id");
            HasMany(it => it.Choices).Inverse().Cascade.AllDeleteOrphan();
            HasMany(it => it.SurveyFieldVocabularyCrossList).Inverse().Cascade.AllDeleteOrphan();
            Table("survey_field");
        }
    }
}
