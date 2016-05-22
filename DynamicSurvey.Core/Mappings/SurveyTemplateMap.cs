using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyTemplateMap : ClassMap<SurveyTemplate>
    {
        public SurveyTemplateMap()
        {
            Id(it => it.Id).Column("id");
            Map(it => it.TemplateName).Unique().Column("template_name");
            Map(it => it.IntroductionText).Length(256).Column("introduction_text");
            Map(it => it.ThankYouText).Length(256).Column("thank_you_text");
            Map(it => it.LandingPageText).Length(256).Column("landing_page_text");
            Map(it => it.Created).Not.Nullable().Column("created");
            Map(it => it.LastModified).Column("last_modified");
            References(it => it.UserCreated).Column("user_created_id");
            References(it => it.UserModified).Column("user_modified_id");
            References(it => it.UserLanguage).Column("language_id");
            Table("survey_template");
        }
    }
}
