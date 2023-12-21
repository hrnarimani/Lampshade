using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permission;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

       
        public EditProduct Command {get; set; }
        public SelectList ProductCategories;

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public EditModel(IProductCategoryApplication productCategoryApplication, IProductApplication productApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(long id)
        {

            Command = _productApplication.GetDetails(id);
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }

        [NeedsPermission(ShopPermission.EditProduct)]
        public IActionResult  OnPostEdit(EditProduct command)
        {
            _productApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
