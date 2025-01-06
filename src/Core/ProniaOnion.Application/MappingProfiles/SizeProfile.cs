using AutoMapper;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class SizeProfile:Profile
    {
        public SizeProfile()
        {
            CreateMap<Size,GetSizeDto>().ReverseMap();
            CreateMap<Size,SizeItemDto>();
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>().ForMember(s=>s.Id,opt=>opt.Ignore());

        }
    }
}
