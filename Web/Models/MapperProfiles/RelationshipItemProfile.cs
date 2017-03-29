using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Web.Data;
using Web.Models.DTO;

namespace Web.Models.MapperProfiles
{
    public class RelationshipItemProfile:Profile
    {
        public RelationshipItemProfile()
        {
            CreateMap<RelationshipItem, RelationshipItemModel>();
        }
    }
}