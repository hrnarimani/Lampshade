using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.Services
{
    public  interface IShopInventoryAcl
    {
        OperationResult ReduceFromInventory(List<OrderItem> items);
    }
}
