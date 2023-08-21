using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class CreateModel : PageModel
    {
        public CreateProductCategory Command { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;
  
        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet()
        {
            
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
       
        {
           var  result=  _productCategoryApplication.Create(command);

             return new JsonResult(result);
        }

    }
}
