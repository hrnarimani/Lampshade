using _0_Framework.Application;
using _01_LamphadeQuery.Contracts.Product;
using _01_LamphadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructur.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace _01_LamphadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly IventoryContext _iventoryContext;
        private readonly DiscountContext _discountContext;


        public ProductQuery(ShopContext context, IventoryContext iventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _iventoryContext = iventoryContext;
            _discountContext = discountContext;
        }
        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _iventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
           
            var products = _context.Products.Include(x=>x.Category).Select(x=>new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            }).AsNoTracking().OrderByDescending(x=>x.Id).Take(6).ToList();

            foreach(var product in products)
              {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault (x=>x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountrate = discount.DiscountRate;
                        product.DiscountRate = discountrate;
                        product.HasDiscount = discountrate> 0;
                        var discountAmount = Math.Round((price * discountrate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }

                }
                
              }
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _iventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var query  = _context.Products
                .Include(x => x.Category).Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    CategorySlug = x.Category.Slug,


                }).AsNoTracking();


            if (!string.IsNullOrWhiteSpace(value))

                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

                var products = query.OrderByDescending(x => x.Id).ToList();
            
          

            foreach (var product in products)
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

            return products;
        }

     

    }
}
