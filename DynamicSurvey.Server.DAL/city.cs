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
    
    public partial class city
    {
        public city()
        {
            this.company = new HashSet<company>();
        }
    
        public decimal id { get; set; }
        public string name { get; set; }
        public decimal country_id { get; set; }
    
        public virtual country country { get; set; }
        public virtual ICollection<company> company { get; set; }
    }
}
