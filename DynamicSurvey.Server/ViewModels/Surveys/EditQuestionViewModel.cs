namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class EditQuestionViewModel
    {
        public int? Id { get; set; }
        public bool Compulsory { get; set; }
        public QuestionFormat Format { get; set; }
    }
}
