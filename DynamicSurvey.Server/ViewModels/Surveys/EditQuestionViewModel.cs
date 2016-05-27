using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditQuestionViewModel
    {
        public const int DefaultRows = 1;
        public const int DefaultAnswerChoiceNumber = 4;
        public const string DefaultExtremePointLeft = "Strongly Agree";
        public const string DefaultExtremePointRight = "Strongly Disagree";
        public const decimal DefaultMinimumValue = 1;
        public const decimal DefaultMaximumValue = 5;
        public const decimal DefaultResolution = 1;

        public int SurveyTemplateId { get; set; }
        public int? QuestionId { get; set; }

        [AllowHtml]
        public string Question { get; set; }

        public string ShortName { get; set; }
        public bool Compulsory { get; set; }
        public QuestionFormat Format { get; set; }
        public int Rows { get; set; }
        public bool IncludeAnnotation { get; set; }
        public List<AnswerChoiceItemViewModel> AnswerChoiceItemViewModels { get; set; }
        public bool AllowMultipleValues { get; set; }
        public string ExtremePointLeft { get; set; }
        public string ExtremePointRight { get; set; }
        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public decimal Resolution { get; set; }
        public bool ShowValue { get; set; }

        public EditQuestionViewModel()
        {
            AnswerChoiceItemViewModels = new List<AnswerChoiceItemViewModel>(DefaultAnswerChoiceNumber);
            Rows = DefaultRows;
            ExtremePointLeft = DefaultExtremePointLeft;
            ExtremePointRight = DefaultExtremePointRight;
            MinimumValue = DefaultMinimumValue;
            MaximumValue = DefaultMaximumValue;
            Resolution = DefaultResolution;
        }
    }
}
