using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Data.Interfaces;
using Web.Models;

namespace Web.Data
{
    public class Token: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}