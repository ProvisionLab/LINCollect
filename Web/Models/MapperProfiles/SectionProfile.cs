using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class SectionProfile:Profile
    {
        public SectionProfile()
        {
            CreateMap<Section, SectionModel>();
            CreateMap<SectionModel, Section>();
        }
    }
}