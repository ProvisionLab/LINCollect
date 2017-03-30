using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class Result : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int PublishSurveyId { get; set; }
        public PublishSurvey PublishSurvey { get; set; }
        public DateTime PassDate { get; set; }
        public List<ResultSection> Sections { get; set; }
    }
}