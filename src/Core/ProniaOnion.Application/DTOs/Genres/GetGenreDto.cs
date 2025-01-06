using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Genres
{
    public record GetGenreDto(int Id,string Name, ICollection<BlogItemDto> Blogs);

}
