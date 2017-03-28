using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Data
{
    public class UserQuestionAnswerValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int QuestionAnswerId { get; set; }
        public UserQuestionAnswer QuestionAnswer { get; set; }
    }
}