﻿using System.Collections.Generic;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class QuestionAnswerModel : IModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int ResultSectionId { get; set; }
        public ResultSectionModel ResultSection { get; set; }
        public List<string> Values { get; set; }
        public string Anotation { get; set; }
        public string CompanyName { get; set; }
    }
}