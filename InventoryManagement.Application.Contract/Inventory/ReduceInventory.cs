using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class ReduceInventory
    {

        public long Count { get; set; }
        public string Description { get; set; }
        public long InventoryId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public ReduceInventory()
        {

        }

        public ReduceInventory(long count, string description, long orderId, long productId)
        {
            Count = count;
            Description = description;
            OrderId = orderId;
            ProductId = productId;
        }
    }




}