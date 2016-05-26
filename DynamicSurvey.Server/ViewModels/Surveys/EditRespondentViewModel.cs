namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditRespondentViewModel
    {
        public int SurveyTemplateId { get; set; }
        public QuestionAction? QuestionAction { get; set; }
        public EditQuestionViewModel EditQuestionViewModel { get; set; }
    }
}
