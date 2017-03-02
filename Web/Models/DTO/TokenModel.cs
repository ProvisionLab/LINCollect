using Web.Managers.Base.Interfaces;

namespace Web.Models.DTO
{
    public class TokenModel : IModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}