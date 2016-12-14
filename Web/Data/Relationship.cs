namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class QuestionLayout
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(36)]
        public string Code { get; set; }
    }

    public partial class NodeSelection
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(36)]
        public string Code { get; set; }
    }

    public partial class RelationshipItem
    {
        public RelationshipItem()
        {
            Questions = new HashSet<RQuestion>();
            NodeQuestions = new HashSet<NQuestion>();
        }

        public int Id { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }

        public int OrderId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string NodeList { get; set; }

        [Required]
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

        [Required]
        public int NodeSelectionId { get; set; }
        public virtual NodeSelection NodeSelection { get; set; }
        
        public string GeneratorName { get; set; }

        public virtual ICollection<RQuestion> Questions { get; set; }
        public virtual ICollection<NQuestion> NodeQuestions { get; set; }
    }
}