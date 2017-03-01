using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class SurveyFileProfile: Profile
    {
        public SurveyFileProfile()
        {
            CreateMap<SurveyFile, SurveyFileModel>();
            CreateMap<SurveyFileModel, SurveyFile>();
        }
    }
}