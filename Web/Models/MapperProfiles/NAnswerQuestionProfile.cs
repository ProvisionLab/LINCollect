using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class NQuestionProfile : Profile
    {
        public NQuestionProfile()
        {
            CreateMap<NQuestion, NQuestionModel>();
        }
    }
}