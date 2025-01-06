using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record GetBlogDto(int Id,string Name, string Article, string Image);

}
