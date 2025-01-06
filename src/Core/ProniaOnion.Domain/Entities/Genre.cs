using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Genre:BaseNameableEntity
    {
        //relational properties
        public ICollection<Blog> Blogs { get; set; }
    }
}
