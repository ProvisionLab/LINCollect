using System;
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
            CreateMap<SurveyModel, Survey>()
                .ForMember(dest=>dest.Language, i=>i.MapFrom(src=>new Language { Id = src.LanguageId}))
                .ForMember(dest=>dest.Status, i=>i.MapFrom(src=> new SurveyStatus {Id = src.SurveyStatusId}))
                .ForMember(dest=>dest.CreateDateUtc, i=>i.ResolveUsing((src, dest)=> DateTime.Now))
                .ForMember(dest=>dest.UpdateDateUtc, i=>i.ResolveUsing((src, dest)=> DateTime.Now));
            CreateMap<Survey, SurveyModel>()
                .ForMember(dest => dest.Language, i => i.MapFrom(src => src.Language.Name))
                .ForMember(dest => dest.Status, i => i.MapFrom(src => src.Status.Name))
                .ForMember(dest=>dest.SurveyStatusId, i=>i.MapFrom(src=>src.Status.Id));
        }
    }
}