using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_LamphadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructur.EFCore;

namespace _01_LamphadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly IventoryContext _iventoryContext;
        private readonly DiscountContext _discountContext; 

    
        public ProductCategoryQuery(ShopContext context, IventoryContext iventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _iventoryContext = iventoryContext;
            _discountContext = discountContext;
        }


        public ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug)
        {
            var inventory = _iventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var category = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products),
                    Slug = x.Slug,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords


                }).FirstOrDefault(x => x.Slug == slug);



            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountrate = discount.DiscountRate;
                        product.DiscountRate = discountrate;
                        product.HasDiscount = discountrate > 0;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountrate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return category;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,

            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory= _iventoryContext.Inventory.Select(x=> new { x.ProductId, x.UnitPrice}).ToList();

            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var categories= _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x=>new ProductCategoryQueryModel 
                {
                Id=x.Id ,
                Name = x.Name,
                Products= MapProducts(x.Products)

                }).ToList();

            foreach(var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var discount= discounts.FirstOrDefault(x=>x.ProductId == product.Id);
                        if(discount != null)
                        {
                            int discountrate = discount.DiscountRate;
                            product.DiscountRate = discountrate;
                            product.HasDiscount = discountrate> 0;
                            var discountAmount = Math.Round((price * discountrate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                }
            }
            return categories;
        }

        private static List<ProductQueryModel> MapProducts (List<Product> products)
        {
            return products.Select(x => new ProductQueryModel 
            { 
                Id=x.Id,
                Name = x.Name,
                Category=x.Category.Name,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,    
                PictureTitle=x.PictureTitle,
                Slug=x.Slug,

            }).ToList ();
        }

       
    }
}
