using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.DTO
{
    public class QuestionAnswerModel
    {
        public int QuestionId { get; set; }
        public List<string> Values { get; set; }
        public string Anotation { get; set; }
        public string CompanyName { get; set; }
    }
}