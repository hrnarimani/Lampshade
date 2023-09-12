using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contract.Inventory
{
    public  class InventoryViewMOdel
    {
        public long ProductId { get;  set; }
        public string Product { get; set; }
        public double UnitPrice { get;  set; }
        public bool InStock { get;  set; }
        public long Id { get; set; }
        public string CreationDate { get; set; }
        public long CurrentCount { get; set; }
    }
}
