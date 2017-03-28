using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Data
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int PublishSurveyId { get; set; }
        public PublishSurvey PublishSurvey { get; set; }
        public DateTime PassDate { get; set; }
        public List<UserQuestionAnswer> QuestionAnswers { get; set; }
    }
}