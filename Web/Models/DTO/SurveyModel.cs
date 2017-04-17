﻿using System.Collections.Generic;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class SurveyModel : IModel
    {
        public string UserId { get; set; }
        public int? SurveyFileId { get; set; }
        public virtual SurveyFileModel SurveyFile { get; set; }
        public virtual string Status { get; set; }
        public int SurveyStatusId { get; set; }
        public int LanguageId { get; set; }
        public virtual string Language { get; set; }
        public string Name { get; set; }
        public string Banner { get; set; }
        public string Introduction { get; set; }
        public string Thanks { get; set; }
        public string Landing { get; set; }
        public virtual List<RespondentModel> Respondents { get; set; }
        public virtual List<RelationshipItemModel> RelationshipItems { get; set; }
        public int Id { get; set; }
    }
}