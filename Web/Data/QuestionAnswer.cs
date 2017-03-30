using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class QuestionAnswer : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int ResultSectionId { get; set; }
        public ResultSection ResultSection { get; set; }
        public string Anotation { get; set; }
        public string CompanyName { get; set; }
        public List<QuestionAnswerValue> Values { get; set; }
    }
}