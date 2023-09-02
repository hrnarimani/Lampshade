using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture 
{
    public class CreateModel : PageModel
    {
        

        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList Products;
		public CreateProductPicture Command { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;

        public CreateModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        public void OnGet()
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public  IActionResult  OnPostCreate(CreateProductPicture command)

        {

            
            _productPictureApplication.Create(command);



            if (OperationResult.IsSuccedded)
            {
                SuccessMessageame = OperationResult.Message;
                return RedirectToPage("./Create");
            }

               

            else
            {
                ErrorMessageame = OperationResult.Message;
                return RedirectToPage("./Create");

            }
                



        }

    }
}
