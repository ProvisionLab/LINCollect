//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DynamicSurvey.Server.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class survey_template
    {
        public survey_template()
        {
            this.survey_field = new HashSet<survey_field>();
        }
    
        public decimal id { get; set; }
        public string template_name { get; set; }
        public decimal user_created_id { get; set; }
        public decimal user_modified_id { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> last_modified { get; set; }
        public decimal language_id { get; set; }
    
        public virtual ICollection<survey_field> survey_field { get; set; }
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
        public virtual language language { get; set; }
    }
}
