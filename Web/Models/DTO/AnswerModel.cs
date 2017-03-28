using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.DTO
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OrderId { get; set; }
        public bool IsDefault { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}