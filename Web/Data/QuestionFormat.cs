

namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public partial class QuestionFormat: IEntity
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