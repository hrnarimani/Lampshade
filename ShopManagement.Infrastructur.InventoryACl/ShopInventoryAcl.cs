using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructur.InventoryACl
{
    public  class ShopInventoryAcl : IShopInventoryAcl

    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public OperationResult ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();                                                                                                                                                                                                                                                                
            foreach (var orderItem in items)
            {
                var item = new ReduceInventory(orderItem.Count, "خرید مشتری", orderItem.OrderId, orderItem.ProductId);
                command.Add(item);
            }
                
              

            return _inventoryApplication.Reduce(command);


        }
    }
}
