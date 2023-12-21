using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository:IRepository<long , Order>
    {
       double GetAmountBy (long id);

       Order GetBy(long  id);

       List<OrderViewModel> Serach(OrderSearchModel searchModel);

       List<OrderItemViewModel> GetItems(long orderId);
    }
}
