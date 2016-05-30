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
        public List<QuestionChoiceItemViewModel> AnswerChoices { get; set; }

        public QuestionItemViewModel()
        {
            AnswerChoices = new List<QuestionChoiceItemViewModel>();
        }
    }
}
