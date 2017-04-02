using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class QuestionAnswerProfile:Profile
    {
        public QuestionAnswerProfile()
        {
            CreateMap<QuestionAnswer, QuestionAnswerModel>()
                .ForMember(dest => dest.ResultSection, i => i.Ignore());
                //.ForMember(dest => dest.Values, i => i.Ignore());
            CreateMap<QuestionAnswerModel, QuestionAnswer>()
                .ForMember(dest=>dest.Values, i=>i.Ignore());
            CreateMap<QuestionAnswerValue, String>()
                .ConstructUsing(t => t.Value);
        }
    }
}