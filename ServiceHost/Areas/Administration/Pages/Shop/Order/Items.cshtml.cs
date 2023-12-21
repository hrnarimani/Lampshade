using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Order
{
    public class ItemsModel : PageModel
    {


        //[TempData]
        //public string ErrorMessageame { get; set; }

        //[TempData]
        //public string SuccessMessageame { get; set; }


        public ItemsModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        private readonly IOrderApplication _orderApplication;


        public List<OrderItemViewModel> Command;

        
      

      //  [NeedsPermission(InventoryPermission.OperationLog)] avaz beshe badan 
        public void OnGet(long id)
        {
            Command = _orderApplication.GetItems(id);
        }

    }
}
