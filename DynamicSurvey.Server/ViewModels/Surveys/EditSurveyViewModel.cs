using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditSurveyViewModel
    {
        public IEnumerable<LanguageItemViewModel> Languages { get; set; }
        public int? Id { get; set; }
        public string TemplateName { get; set; }
        public int? LanguageId { get; set; }

        [AllowHtml]
        public string IntroductionText { get; set; }

        [AllowHtml]
        public string ThankYouText { get; set; }

        [AllowHtml]
        public string LandingPageText { get; set; }
    }
}
