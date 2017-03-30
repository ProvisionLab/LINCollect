using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Data.Interfaces;

namespace Web.Data
{
    public class Section:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}