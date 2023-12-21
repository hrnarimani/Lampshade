using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

       
        public EditInventory Command {get; set; }
        public SelectList Products;



        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public EditModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(long id)
        {

            Command = _inventoryApplication.GetDetais(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        [NeedsPermission(InventoryPermission.EditInventory)]
        public IActionResult  OnPostEdit(EditInventory command)
        {
            _inventoryApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
