using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class RQuestionProfile: Profile
    {
        public RQuestionProfile()
        {
            CreateMap<RQuestion, RQuestionModel>();
        }
    }
}