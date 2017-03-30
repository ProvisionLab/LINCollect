using System;
using System.Collections.Generic;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class ResultModel : IModel
    {
        public int Id { get; set; }
        public int PublishSurveyId { get; set; }
        public PublishSurveyModel PublishSurvey { get; set; }
        public DateTime PassDate { get; set; }
        public List<ResultSectionModel> Sections { get; set; }
    }
}