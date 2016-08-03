using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditQuestionViewModel
    {
        public const int DefaultRows = 1;
        public const int DefaultAnswerChoiceNumber = 4;
        public const int DefaultColNumber = 4;
        public const int DefaultRowNumber = 4;
        public const string DefaultExtremePointLeft = "Strongly Agree";
        public const string DefaultExtremePointRight = "Strongly Disagree";
        public const decimal DefaultMinimumValue = 1;
        public const decimal DefaultMaximumValue = 5;
        public const decimal DefaultResolution = 1;
        //public const bool DefaultMatrixMiltiple = true;
        //public const bool DefaultMatrixSingleRow = false;
        //public const bool DefaultMatrixSingleColumn = false;

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
        public bool MatrixMultiple { get; set; }
        public bool MatrixSingleRow { get; set; }
        public bool MatrixSingleColumn { get; set; }
        public string ExtremePointLeft { get; set; }
        public string ExtremePointRight { get; set; }
        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public decimal Resolution { get; set; }
        public bool ShowValue { get; set; }
        public List<MatrixRow> MatrixRows { get; set; }
        public List<MatrixCol> MatrixColumns { get; set; }
        //public string MatrixVariant { get; set; }
        public int? InsertPosition { get; set; }

        public EditQuestionViewModel()
        {
            AnswerChoiceItemViewModels = new List<AnswerChoiceItemViewModel>(DefaultAnswerChoiceNumber);
            MatrixColumns = new List<MatrixCol>(DefaultColNumber);
            MatrixRows = new List<MatrixRow>(DefaultRowNumber);
            Rows = DefaultRows;
            ExtremePointLeft = DefaultExtremePointLeft;
            ExtremePointRight = DefaultExtremePointRight;
            MinimumValue = DefaultMinimumValue;
            MaximumValue = DefaultMaximumValue;
            Resolution = DefaultResolution;
            //MatrixMultiple = DefaultMatrixMiltiple;
            //MatrixSingleRow = DefaultMatrixSingleRow;
            //MatrixSingleColumn = DefaultMatrixSingleColumn;
        }
    }
}
