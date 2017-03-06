using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels
{
    public class PublishView
    {
        public int SurveyId { get; set; }
        [Display(Name = "Email subject")]
        public string Subject { get; set; }
        [Display(Name = "Email message")]
        public string Message { get; set; }
    }
}