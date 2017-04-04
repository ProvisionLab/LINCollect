using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserModel>();

            CreateMap<ApplicationUserModel, ApplicationUser>();
        }
    }
}