﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class BlogTag
    {
        public int TagId { get; set; }
        public int BlogId { get; set; }
        public Tag Tag { get; set; }
        public Blog Blog { get; set; }
    }
}
