using DynamicSurvey.Server.ViewModels;

namespace DynamicSurvey.Server.Models.Surveys
{
    public class EditRespondentRequestModel
    {
        public QuestionAction? QuestionAction { get; set; }
        public int? SurveyTemplateId { get; set; }
        public int? QuestionId { get; set; }
        public int? InsertPosition { get; set; }
    }
}
