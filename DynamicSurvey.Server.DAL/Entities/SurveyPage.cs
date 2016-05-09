using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
    public class SurveyPage
    {
        public string Title { get; set; }
        public List<SurveyField> Fields { get; set; }
    }
}
