namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public class SurveyFile: IEntity
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public string Link { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
    }
}