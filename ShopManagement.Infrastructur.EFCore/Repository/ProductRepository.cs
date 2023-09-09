using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructur.EFCore.Repository
{
    public  class ProductRepository : RepositoryBase<long, Product>, IProductRepository

    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context):base(context)
        {
            _context = context;
        }


        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice
                //bar asase khoroji yani editproduct meghdar dehi shodan
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
                Category = x.Category.Name,
                CategoryId=x.CategoryId,
                IsInStock = x.IsInStock,
                CreatioDate= x.CreationDate.ToFarsi()

                //bar asase ProductViewModel meghdar dehi shodan
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));


            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId!=0)
                query = query.Where(x => x.CategoryId==searchModel.CategoryId);


            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
