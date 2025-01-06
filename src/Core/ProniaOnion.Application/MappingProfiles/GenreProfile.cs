using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class GenreProfile:Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GetGenreDto>().ReverseMap();
            CreateMap<Genre, GenreItemDto>();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
