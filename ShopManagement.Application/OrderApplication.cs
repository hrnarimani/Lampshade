using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Sender.Sms;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        public OrderApplication(IAuthHelper authHelper, IConfiguration configuration, IOrderRepository orderRepository, IShopInventoryAcl shopInventoryAcl, ISmsSender smsSender, IShopAccountAcl shopAccountAcl)
        {
            _authHelper = authHelper;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _shopInventoryAcl = shopInventoryAcl;
            _smsSender = smsSender;
            _shopAccountAcl = shopAccountAcl;
        }

        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        private readonly ISmsSender _smsSender;
        private readonly IShopAccountAcl _shopAccountAcl;

       


        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
            var order = new Order(currentAccountId, cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount,
                cart.PayAmount);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }


        public void Cancel(long id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        } 

        public string PaymentSucceded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded( refId);
            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);

            _shopInventoryAcl.ReduceFromInventory(order.Items);
            if (OperationResult.IsSuccedded == false)
                return "";

            _orderRepository.SaveChanges();

            var (name, mobile) = _shopAccountAcl.GetAccountBy(order.AccountId);
            _smsSender.SendByKavenagarAsync( $"{name} گرامی سفارش شما به شماره {issueTrackingNo} ثبت و در زمان معین شده  ارسال خواهد شد." , mobile);
            return issueTrackingNo;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

       
        public List<OrderItemViewModel> GetItems(long orderId)
        {
            return _orderRepository.GetItems(orderId);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Serach(searchModel);
        }
    }
}
