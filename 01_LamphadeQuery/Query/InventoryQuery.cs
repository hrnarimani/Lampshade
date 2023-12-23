using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_LamphadeQuery.Contracts.Inventory;
using InventoryManagement.Infrasructure.EFCore;
using ShopManagement.Infrastructur.EFCore;

namespace _01_LamphadeQuery.Query
{
    public  class InventoryQuery :IInventoryQuery
    {
        public InventoryQuery(ShopContext shopContext, IventoryContext iventoryContext)
        {
            _shopContext = shopContext;
            _iventoryContext = iventoryContext;
        }

        private readonly ShopContext _shopContext;
        private readonly IventoryContext _iventoryContext;

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _iventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var product = _shopContext.Products.Select(x => new { x.Id, x.Name })
                    .FirstOrDefault(x => x.Id == command.ProductId);

                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product?.Name
                };

            }

            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}
