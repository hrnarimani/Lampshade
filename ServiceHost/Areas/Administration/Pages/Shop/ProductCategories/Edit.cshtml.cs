using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        public EditProductCategory Command { get; set; }

        private readonly IProductCategoryApplication _productCategoryApplication;

        public EditModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(long id)
        {
            Command = _productCategoryApplication.GetDetails(id);
        }

        public IActionResult  OnPostEdit(EditProductCategory command)
        {
            _productCategoryApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
