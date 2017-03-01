
namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using System.ComponentModel.DataAnnotations;

    public partial class Question: IEntity
    {
        public Question() {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        [Required]
        public int RespondentId { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }

        public int OrderId { get; set; } 
        
        public bool IsCompulsory { get; set; }

        public bool IsAfterSurvey { get; set; }

        public string Introducing { get; set; }

        [Required]
        [StringLength(256)]
        public string ShortName { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public int QuestionFormatId { get; set; }
        public virtual QuestionFormat QuestionFormat { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        #region Format options
        //Text
        public int? TextRowsCount { get; set; }
        //Choice Across
        public bool? IsAnnotation { get; set; }
        //Choice Across, Choice Down
        public bool? IsMultiple { get; set; }
        //Slider
        public string TextMin { get; set; }
        public string TextMax { get; set; }
        public string ValueMin { get; set; }
        public string ValueMax { get; set; }
        public int Resolution { get; set; }
        public bool? IsShowValue { get; set; } 
        public string Rows { get; set; }
        #endregion
    }
}