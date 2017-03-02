using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Data.Interfaces
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
