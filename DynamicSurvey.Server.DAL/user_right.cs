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
    
    public partial class user_right
    {
        public user_right()
        {
            this.user = new HashSet<user>();
        }
    
        public decimal id { get; set; }
        public Nullable<System.DateTime> last_modified { get; set; }
        public string name { get; set; }
        public int access_level { get; set; }
    
        public virtual ICollection<user> user { get; set; }
    }
}
