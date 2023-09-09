using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts 
{
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

       
        public EditColleagueDiscount Command {get; set; }
        public SelectList Products;




        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;


        public EditModel(IProductApplication ProductApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = ProductApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(long id)
        {

            Command = _colleagueDiscountApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult  OnPostEdit(EditColleagueDiscount command)
        {
            _colleagueDiscountApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
