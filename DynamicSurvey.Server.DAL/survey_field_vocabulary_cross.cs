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
    
    public partial class survey_field_vocabulary_cross
    {
        public decimal id { get; set; }
        public decimal fk_survey_field_id { get; set; }
        public decimal fk_vocabulary_word_id { get; set; }
    
        public virtual survey_field survey_field { get; set; }
        public virtual vocabulary vocabulary { get; set; }
    }
}
