

namespace Web.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public partial class RAnswer: IEntity
    {
        public int Id { get; set; }
        
        [Required]
        public int RQuestionId { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }

        public int OrderId { get; set; } 
        
        public bool IsDefault { get; set; }
        
        [Required]
        [StringLength(256)]
        public string Text { get; set; }
        [Required]
        [StringLength(256)]
        public string Value { get; set; }
    }
}