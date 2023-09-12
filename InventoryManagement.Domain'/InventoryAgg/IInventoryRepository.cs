using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain_.InventoryAgg
{
    public  interface  IInventoryRepository:IRepository<long,Inventory>
    {
        EditInventory GetDetais(long id);
        List<InventoryViewMOdel> Seearch(InventorySearchModel searchModel);
        Inventory GetBy(long productId);
    }
}
