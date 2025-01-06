using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Size:BaseNameableEntity
    {
        //relational properties
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
