using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyPageMap : ClassMap<SurveyPage>
    {
        public SurveyPageMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.PageIndex).Not.Nullable().Column("page_index");
            Map(it => it.PageTitle).Not.Nullable().Column("page_title");
            References(it => it.SurveyTemplate).Not.Nullable().Column("fk_survey_template_parent_id");
            Table("survey_page");
        }
    }
}
