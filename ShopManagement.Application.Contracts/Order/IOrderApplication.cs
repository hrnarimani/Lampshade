using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public interface  IOrderApplication
    {
        long PlaceOrder(Cart cart);
        void Cancel(long id);
        string PaymentSucceded(long orderId , long refId);
        double GetAmountBy(long id);
        List<OrderViewModel> Serach(OrderSearchModel searchModel);

        List<OrderItemViewModel> GetItems(long orderId);

    }
}
