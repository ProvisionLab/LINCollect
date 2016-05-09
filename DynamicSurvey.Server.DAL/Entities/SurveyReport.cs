using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
    public class SurveyReport
    {
        public User Enumerator { get; set; }
        public User Respondent { get; set; }
        public Survey Report { get; set; }
    }
    
}
