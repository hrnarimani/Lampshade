using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList Products;

        public CreateInventory Command { get; set; }


        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public CreateModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet()
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public void OnPostCreate(CreateInventory command)

        {


            _inventoryApplication.Create(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageame = OperationResult.Message;

            else

                ErrorMessageame = OperationResult.Message;



        }

    }
}
