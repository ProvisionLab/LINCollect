using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.DTO
{
    public class RAnswerModel
    {
        public int Id { get; set; }
        public int RQuestionId { get; set; }
        public int OrderId { get; set; }
        public bool IsDefault { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}