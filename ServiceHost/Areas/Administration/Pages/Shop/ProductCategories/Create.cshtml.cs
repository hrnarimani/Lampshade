using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }
        
      

        public CreateProductCategory Command { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet()
        {

        }

        public void OnPostCreate(CreateProductCategory command)

        {
            if (ModelState.IsValid)
            {
                _productCategoryApplication.Create(command);



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
