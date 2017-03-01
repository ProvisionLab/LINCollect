using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class SurveyFileModel: IModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}