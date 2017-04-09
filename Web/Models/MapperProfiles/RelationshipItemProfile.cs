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
            CreateMap<RelationshipItemModel, RelationshipItem>()
                .ForMember(d => d.NodeQuestions, i => i.Ignore())
                .ForMember(d => d.NodeSelection, i => i.Ignore())
                .ForMember(d => d.Questions, i => i.Ignore())
                .ForMember(d => d.QuestionLayout, i => i.Ignore())
                .ForMember(d => d.CreateDateUtc, i => i.ResolveUsing((from, to) => DateTime.UtcNow))
                .ForMember(d => d.UpdateDateUtc, i => i.ResolveUsing((from, to) => DateTime.UtcNow));
        }
    }
}