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
            CreateMap<NQuestionModel, NQuestion>()
                .ForMember(d => d.QuestionFormat, i => i.Ignore())
                .ForMember(d => d.Answers, i => i.Ignore());
        }
    }
}