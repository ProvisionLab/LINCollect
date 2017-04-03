using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Models.ViewModels;

namespace Web.Models.MapperProfiles
{
    public class ApplicationUserProfile: Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, EditInterviewerViewModel>()
                .ForMember(dest => dest.NewPassword, i => i.Ignore())
                .ForMember(dest => dest.NewPasswordConfirm, i => i.Ignore());

            CreateMap<EditInterviewerViewModel, ApplicationUser>();

        }
    }
}