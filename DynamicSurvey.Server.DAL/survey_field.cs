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
    
    public partial class survey_field
    {
        public survey_field()
        {
            this.survey_detail = new HashSet<survey_detail>();
            this.survey_field1 = new HashSet<survey_field>();
            this.survey_field_vocabulary_cross = new HashSet<survey_field_vocabulary_cross>();
        }
    
        public decimal id { get; set; }
        public decimal fk_parent_page_id { get; set; }
        public decimal fk_survey_field_type_id { get; set; }
        public Nullable<decimal> fk_group_id { get; set; }
        public string label { get; set; }
    
        public virtual ICollection<survey_detail> survey_detail { get; set; }
        public virtual survey_field_type survey_field_type { get; set; }
        public virtual ICollection<survey_field> survey_field1 { get; set; }
        public virtual survey_field survey_field2 { get; set; }
        public virtual survey_page survey_page { get; set; }
        public virtual ICollection<survey_field_vocabulary_cross> survey_field_vocabulary_cross { get; set; }
    }
}
