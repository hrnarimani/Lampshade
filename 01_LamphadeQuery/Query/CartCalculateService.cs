using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LamphadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_LamphadeQuery.Query
{
    public class CartCalculateService :ICartCalculateService
    {

        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculateService(IAuthHelper authHelper, DiscountContext discountContext)
        {
            _authHelper = authHelper;
            _discountContext = discountContext;
        }



        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var Cart = new Cart();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemove)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();

            var currentAccountRole = _authHelper.CurrentAccountRole();

            foreach (var cartItem in cartItems)
            {
                if (currentAccountRole == RolesConst.Colleague)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (colleagueDiscount != null)
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                }

                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (customerDiscount != null)
                        cartItem.DiscountRate = customerDiscount.DiscountRate;
                }

                cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;

                Cart.Add(cartItem);
                
            }
            return Cart;

        }
    }
}
