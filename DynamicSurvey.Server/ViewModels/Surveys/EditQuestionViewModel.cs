using System.Web.Mvc;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditQuestionViewModel
    {
        public int? Id { get; set; }

        [AllowHtml]
        public string Question { get; set; }

        public string ShortName { get; set; }
        public bool Compulsory { get; set; }
        public QuestionFormat Format { get; set; }
    }
}
