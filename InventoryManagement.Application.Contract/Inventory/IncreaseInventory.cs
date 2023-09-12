using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contract.Inventory
{
    public  class IncreaseInventory
    {
        
        public long Count { get; set; }
        public string Description { get; set; } 
        public long InventoryId { get; set; } // برای جستجو میخواهیمش
        //public long OperatorId { get; set; }  // این رو هم میخواهیم ولی چون مقدار ثابت ارسال میکنیم 
        //در اینجا دیگه نماریمش
    }
}
