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
    
    public partial class language
    {
        public language()
        {
            this.survey_template = new HashSet<survey_template>();
            this.user_language = new HashSet<user_language>();
            this.vocabulary = new HashSet<vocabulary>();
        }
    
        public decimal id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<survey_template> survey_template { get; set; }
        public virtual ICollection<user_language> user_language { get; set; }
        public virtual ICollection<vocabulary> vocabulary { get; set; }
    }
}
