using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;

namespace Web.Models.DTO
{
    public class RespondentModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public bool IsAfterSurvey { get; set; }
        public virtual List<QuestionModel> Questions { get; set; }
        public virtual List<QuestionAnswerModel> QuestionAnswers { get; set; }
    }
}