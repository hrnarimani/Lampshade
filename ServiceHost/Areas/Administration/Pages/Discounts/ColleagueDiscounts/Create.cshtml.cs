using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList Products;
      
        public DefineColleagueDiscount Command { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;


        public CreateModel(IProductApplication ProductApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = ProductApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }


        public void OnGet()
        {
           Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public  void  OnPostCreate(DefineColleagueDiscount command)

        {


            _colleagueDiscountApplication.Define(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageame = OperationResult.Message;

            else

                ErrorMessageame = OperationResult.Message;



        }

    }
}
