namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;

    public partial class QuestionFormat
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Code { get; set; }
    }
}