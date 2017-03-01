

namespace Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public partial class Language: IEntity
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(6)]
        public string Code { get; set; }
        [StringLength(2)]
        public string ShortCode { get; set; }
    }
}