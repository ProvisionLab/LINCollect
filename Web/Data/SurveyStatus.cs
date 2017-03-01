

namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public partial class SurveyStatus: IEntity
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
    }
}