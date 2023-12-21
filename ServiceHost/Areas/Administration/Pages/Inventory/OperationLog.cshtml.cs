using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class OperationLogModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }




        public List<InventoryOperationViewModel> Command;


        
        private readonly IInventoryApplication _inventoryApplication;

        public OperationLogModel( IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        [NeedsPermission(InventoryPermission.OperationLog)]
        public void OnGet(long id)
        {
            Command = _inventoryApplication.GetOperationLog(id);
        }

      

    }
}
