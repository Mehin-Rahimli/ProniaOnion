using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author,GetAuthorDto>().ReverseMap();
            CreateMap<Author,AuthorItemDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>().ForMember(a=>a.Id,opt=>opt.Ignore());
        }
    }
}
