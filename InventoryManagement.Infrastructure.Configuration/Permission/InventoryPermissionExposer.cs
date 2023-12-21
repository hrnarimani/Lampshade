using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permission
{
    public  class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermission .ListInventory, "ListInventory"),
                        new PermissionDto(InventoryPermission.SearchInventory, "SearchInventory"),
                        new PermissionDto(InventoryPermission.CreateInventory, "CreateInventory"),
                        new PermissionDto(InventoryPermission.EditInventory, "EditInventory"),
                        new PermissionDto(InventoryPermission.Increase, "Increase"),
                        new PermissionDto(InventoryPermission.Reduce, "Reduce"),
                        new PermissionDto(InventoryPermission.OperationLog, "OperationLog")

                    }
                }
            };
        }
    }
}
