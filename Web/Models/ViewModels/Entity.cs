using System;

namespace Web.Models.ViewModels
{
    public class Entity
    {
        public int Id { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateDateUtc { get; set; }
    }
}