using AutoMapper;
using Web.Data;
using Web.Models.DTO;
using Web.Models.ViewModels;

namespace Web.Models.MapperProfiles
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile()
        {
            CreateMap<SurveyFile, SurveyFileModel>();
            CreateMap<SurveyFileModel, SurveyFile>();
            CreateMap<Survey, SurveyModel>();
            CreateMap<SurveyModel, Survey>();
            CreateMap<Survey, SurveyView>()
                .ForMember(dest => dest.Language, i => i.MapFrom(src => src.Language.Name))
                .ForMember(dest => dest.Status, i => i.MapFrom(src => src.Status.Name));
        }
    }
}