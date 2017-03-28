using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Data
{
    public class UserQuestionAnswer
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public byte SectionType { get; set; }
        public int QuestionId { get; set; }
        private List<UserQuestionAnswerValue> Answers { get; set; }
    }
}