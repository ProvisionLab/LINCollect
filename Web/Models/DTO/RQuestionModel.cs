using System.Collections.Generic;

namespace Web.Models.DTO
{
    public class RQuestionModel
    {
        public int Id { get; set; }
        public int RelationshipItemId { get; set; }
        public int OrderId { get; set; }
        public bool IsCompulsory { get; set; }
        public bool IsAfterSurvey { get; set; }
        public string Introducing { get; set; }
        public string ShortName { get; set; }
        public string Text { get; set; }
        public int QuestionFormatId { get; set; }
        public virtual QuestionFormatModel QuestionFormat { get; set; }
        public virtual List<RAnswerModel> Answers { get; set; }

        #region Format options

        //Text
        public int? TextRowsCount { get; set; }
        //Choice Across
        public bool? IsAnnotation { get; set; }
        //Choice Across, Choice Down
        public bool? IsMultiple { get; set; }
        //Slider
        public string TextMin { get; set; }
        public string TextMax { get; set; }
        public string ValueMin { get; set; }
        public string ValueMax { get; set; }
        public int Resolution { get; set; }
        public bool? IsShowValue { get; set; }
        public string Rows { get; set; }

        #endregion
    }
}