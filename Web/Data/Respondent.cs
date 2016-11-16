namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Respondent
    {
        public Respondent()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }

        public bool IsAfterSurvey { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}