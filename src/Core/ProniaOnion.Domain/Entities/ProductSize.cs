using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
   
    public class ProductSize
    {
        
        //relational properties
        public int SizeId { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public Product Product { get; set; }
    }
}
