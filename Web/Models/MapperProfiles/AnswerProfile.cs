using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerModel>();
            CreateMap<AnswerModel, Answer>()
                .ForMember(d => d.CreateDateUtc, i => i.ResolveUsing((from, to) => DateTime.UtcNow))
                .ForMember(d => d.UpdateDateUtc, i => i.ResolveUsing((from, to) => DateTime.UtcNow));
        }
    }
}