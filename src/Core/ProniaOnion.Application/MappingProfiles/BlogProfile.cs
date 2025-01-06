using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, GetBlogDto>().ReverseMap();
            CreateMap<Blog, BlogItemDto>();
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
