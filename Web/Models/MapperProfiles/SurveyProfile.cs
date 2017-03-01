using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class SurveyProfile: Profile
    {
        public SurveyProfile()
        {
            CreateMap<Survey, SurveyFileModel>();
        }
    }
}