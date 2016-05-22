using System.Collections.Generic;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditSurveyViewModel
    {
        public IEnumerable<LanguageItemViewModel> Languages { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? LanguageId { get; set; }
        public string IntroductionText { get; set; }
        public string ThankYouText { get; set; }
        public string LandingPageText { get; set; }
    }
}
