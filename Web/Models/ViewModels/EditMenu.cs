using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Data;

namespace Web.Models.ViewModels
{
    public class EditMenu
    {
        public int SurveyId { get; set; }
        public int step { get; set; }
        public string link { get; set; }
    }
}