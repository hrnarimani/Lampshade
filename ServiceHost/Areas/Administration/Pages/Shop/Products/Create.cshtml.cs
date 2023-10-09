using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products 
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public CreateProduct Command { get; set; }
        private readonly IProductApplication _productApplication;

        public CreateModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet()
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }

        public  void  OnPostCreate(CreateProduct command)

        {
            if (ModelState.IsValid)
            {

                _productApplication.Create(command);



                if (OperationResult.IsSuccedded)

                    SuccessMessageame = OperationResult.Message;

                else

                    ErrorMessageame = OperationResult.Message;
            }

            else
                ErrorMessageame = "لطفا مقادیر خواسته شده را به درستی پر نمایید";


        }

    }
}
