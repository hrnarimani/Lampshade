using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList Products;
      
        public DefineCustomerDiscount Command { get; set; }
       
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;


       
        public CreateModel(IProductApplication ProductApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = ProductApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

     
        public void OnGet()
        {
           Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public  void  OnPostCreate(DefineCustomerDiscount command)

        {

            
            _customerDiscountApplication.Define(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageame = OperationResult.Message;

            else

                ErrorMessageame = OperationResult.Message;



        }

    }
}
