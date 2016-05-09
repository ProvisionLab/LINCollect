using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
    public class SurveyField
    {
        public int Id { get; set; }
        public string Label { get; set; }

        public FieldType FieldType { get; set; }

        public string[] DefaultValues { get; set; }

        public string UserAnswer { get; set; }
    }
}
