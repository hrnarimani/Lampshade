﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public  class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public string Product { get; set; }
        public string CreationDate { get; set; } 
        public bool IsRemove { get; set; }
    }
}
