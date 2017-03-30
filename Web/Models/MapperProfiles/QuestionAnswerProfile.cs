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
                .ForMember(dest=>dest.Values, i=>i.MapFrom(src=>src.Values.Select(t=>t.Value)));
            CreateMap<QuestionAnswerModel, QuestionAnswer>()
                .ForMember(dest=>dest.Values, i=>i.Ignore());
        }
    }
}