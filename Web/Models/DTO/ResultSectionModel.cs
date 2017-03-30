using System.Collections.Generic;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class ResultSectionModel : IModel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int SectionTypeId { get; set; }
        public int ResultId { get; set; }
        public ResultModel Result { get; set; }
        public SectionModel SectionType { get; set; }
        public List<QuestionAnswerModel> Answers { get; set; }
    }
}