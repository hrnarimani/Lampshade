using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.InventoryAgg
{
    public  class InventoryOperation
    {
        public long Id { get; set; }
        public bool Operation { get; set; }
        public DateTime OperationDate { get; set; }
        public long Count { get; set; }
        public long OperatorId { get; set; }    
        public long CurrentCount { get; set; }
        public string Description { get; set; }
        public long OrderID { get; set; }
        public long InventoryID { get; set; }   
        public Inventory Inventory { get; set; }
       
        public InventoryOperation() 
        {
        }

        public InventoryOperation(bool operation, long count, long operatorId, long currentCount, string description, long orderID, long inventoryID)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderID = orderID;
            InventoryID = inventoryID;
            OperationDate= DateTime.Now;
        }
    }
}
