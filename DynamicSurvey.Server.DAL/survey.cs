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
    
    public partial class survey
    {
        public decimal id { get; set; }
        public decimal respondent_id { get; set; }
        public decimal company_id { get; set; }
        public decimal survey_detail_id { get; set; }
    
        public virtual company company { get; set; }
        public virtual user user { get; set; }
        public virtual survey_detail survey_detail { get; set; }
    }
}
