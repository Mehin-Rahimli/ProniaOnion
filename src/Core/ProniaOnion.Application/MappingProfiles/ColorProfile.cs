using AutoMapper;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ColorProfile:Profile
    {
        public ColorProfile()
        {
            CreateMap<Color,GetColorDto>().ReverseMap();
            CreateMap<Color, ColorItemDto>();
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>().ForMember(c=>c.Id,opt=>opt.Ignore());

        }
    }
}
