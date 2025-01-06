using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
  
    public class ProductTag
    {
        //relational properties
        public Tag Tag { get; set; }
        public Product Product { get; set; }
        public int TagId { get; set; }
        public int ProductId { get; set; }

    }
}
