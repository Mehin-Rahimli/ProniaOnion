using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Authors
{
    public record GetAuthorDto(int Id, string Name, string Surname, string ProfileImage, ICollection<BlogItemDto> Blogs);
}
