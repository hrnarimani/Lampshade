﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_LamphadeQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructur.EFCore;

namespace _01_LamphadeQuery.Query
{
    public  class ProductCategoryQuery: IProductCategoryQuery
    {
        private readonly  ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture=x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,

            }).ToList();
        }
    }
}
