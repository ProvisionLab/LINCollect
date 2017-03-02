using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<Token, TokenModel>();
            CreateMap<TokenModel, Token>();
        }
    }
}