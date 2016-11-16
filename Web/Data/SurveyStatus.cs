namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using Models;

    public partial class SurveyStatus
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
    }


    public partial class SurveyFile
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public string Link { get; set; }
        
        [StringLength(128)]
        public string UserId { get; set; }
    }
}