﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LamphadeQuery.Contracts.Inventory
{
    public  class IsInStock
    {
        public int Count { get; set; }
        public long ProductId { get; set; }
    }
}
