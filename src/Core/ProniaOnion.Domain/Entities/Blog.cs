using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Blog:BaseNameableEntity
    {
        public string Article { get; set; }
        public string Image { get; set; }


        //relational properties
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
