using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record UpdateBlogDto(string Name, string Article, string Image, int AuthorId, int GenreId);

}
