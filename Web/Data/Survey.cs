

namespace Web.Data
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public partial class Survey: IEntity
    {
        public Survey()
        {
            Respondents = new HashSet<Respondent>();
            RelationshipItems = new HashSet<RelationshipItem>();
            ApplicationUsers = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public int SurveyStatusId { get; set; }
        public virtual SurveyStatus Status { get; set; }
        public int? SurveyFileId { get; set; }
        public virtual SurveyFile SurveyFile { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Banner { get; set; }
        public string Introduction { get; set; }
        public string Thanks { get; set; }
        public string Landing { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Respondent> Respondents { get; set; }
        public virtual ICollection<RelationshipItem> RelationshipItems { get; set; }
    }
}