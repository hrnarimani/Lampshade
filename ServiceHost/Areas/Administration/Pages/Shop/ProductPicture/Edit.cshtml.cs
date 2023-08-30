using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class EditModel : PageModel
    {
       

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

       
        public EditProductPicture Command {get; set; }
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public EditModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }


        public void OnGet(long id)
        {

            Command = _productPictureApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult  OnPostEdit(EditProductPicture command)
        {
            _productPictureApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }

       
    }
}
