using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class ResultSection : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int SectionTypeId { get; set; }
        public int ResultId { get; set; }
        public Result Result { get; set; }
        public Section SectionType { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
    }
}