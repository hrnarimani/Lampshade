using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contract.Inventory
{
    public  interface  IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit (EditInventory command);
        OperationResult Increase (IncreaseInventory command);
        OperationResult Reduce (ReduceInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        EditInventory GetDetais(long id);
        List<InventoryViewMOdel> Seearch(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long iventoryId);
    }
}
