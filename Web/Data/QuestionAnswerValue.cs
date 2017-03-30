using System.ComponentModel.DataAnnotations;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class QuestionAnswerValue: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
    }
}