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
    
    public partial class user_language_cross
    {
        public decimal id { get; set; }
        public decimal user_id { get; set; }
        public decimal language_id { get; set; }
    
        public virtual user user { get; set; }
        public virtual language language { get; set; }
    }
}