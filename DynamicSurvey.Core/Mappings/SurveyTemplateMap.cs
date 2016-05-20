using DynamicSurvey.Core.Entities;
using FluentNHibernate.Mapping;

namespace DynamicSurvey.Core.Mappings
{
    public class SurveyTemplateMap : ClassMap<SurveyTemplate>
    {
        public SurveyTemplateMap()
        {
            Id(it => it.Id);
            Map(it => it.TemplateName).Unique();
            Map(it => it.Created).Not.Nullable();
            Map(it => it.LastModified);
            References(it => it.UserCreated).Column("UserCreated_Id");
            References(it => it.UserModified).Column("UserModified_Id");
            References(it => it.UserLanguage).Column("UserLanguage_Id");
        }
    }
}
