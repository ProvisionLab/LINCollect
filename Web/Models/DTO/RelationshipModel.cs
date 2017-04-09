using System;
using System.Collections.Generic;
using Web.Data;
using Web.Managers.Base.Interfaces;
using Web.Models.ViewModels;

namespace Web.Models.DTO
{
    public class RelationshipItemModel : IModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string NodeList { get; set; }
        public int QuestionLayoutId { get; set; }
        public virtual QuestionLayout QuestionLayout { get; set; }
        public int MaximumNodes { get; set; }
        public bool SortNodeList { get; set; }
        public bool AddNodes { get; set; }
        public bool HideAddedNodes { get; set; }
        public bool AllowSelectAllNodes { get; set; }
        public bool CanSkip { get; set; }
        public bool UseDDSearch { get; set; }
        public bool SuperUserViewNodes { get; set; }
        public int NodeSelectionId { get; set; }
        public virtual NodeSelection NodeSelection { get; set; }
        public string GeneratorName { get; set; }
        public Companies Companies { get; set; }
        public virtual List<RQuestionModel> Questions { get; set; }
        public virtual List<NQuestionModel> NodeQuestions { get; set; }
        public virtual List<QuestionAnswerModel> QuestionAnswers { get; set; }
        public virtual List<QuestionAnswerModel> NQuestionAnswers { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }
    }
    public class ResultRelationShipModel
    {
        public int Id { get; set; }
        public Companies Companies { get; set; }
        public virtual List<QuestionAnswerModel> QuestionAnswers { get; set; }
        public virtual List<QuestionAnswerModel> NQuestionAnswers { get; set; }
    }
}