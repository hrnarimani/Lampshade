using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Order;

namespace _01_LamphadeQuery.Contracts
{
    public  interface ICartCalculateService 
    {
        Cart ComputeCart (List<CartItem> cartItems);
    }
}
