using System.Collections.Generic;

namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class QuestionItemViewModel
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int DisplayOrder { get; set; }
        public QuestionFormat Format { get; set; }
        public int Rows { get; set; }
        public bool AllowMultipleValues { get; set; }
        public bool MatrixMiltiple { get; set; }
        public bool MatrixSingleRow { get; set; }
        //public bool MatrixSingleColumn { get; set; }
        public List<QuestionChoiceItemViewModel> AnswerChoices { get; set; }
        public List<MatrixColumnViewModel> MatrixColumnList { get; set; }
        public List<MatrixRowViewModel> MatrixRowList { get; set; }

        public QuestionItemViewModel()
        {
            AnswerChoices = new List<QuestionChoiceItemViewModel>();
            MatrixColumnList = new List<MatrixColumnViewModel>();
            MatrixRowList = new List<MatrixRowViewModel>();
        }
    }
}
