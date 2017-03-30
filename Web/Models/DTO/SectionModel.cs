using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class SectionModel: IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}