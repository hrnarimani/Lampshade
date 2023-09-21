using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public  class CreateInventory
    {
        [Range(1,100000 , ErrorMessage = ValidationMessages.NotValid)]
        public long ProductId { get;  set; }
        [Range(1,double.MaxValue, ErrorMessage = ValidationMessages.NotValid)]
        public double UnitPrice { get;  set; }
    }
}
