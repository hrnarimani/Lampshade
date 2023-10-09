﻿using _01_LampshadeQuery.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LamphadeQuery.Contracts.Product
{
    public  interface IProductQuery
    {
        ProductQueryModel GetDetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
