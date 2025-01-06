using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Author:BaseNameableEntity
    {
        public string Surname { get; set; }
        public string ProfileImage { get; set; }

        //relational properties
        public ICollection<Blog>Blogs { get; set; }

    }
}
