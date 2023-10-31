using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public SelectList Roles;
        public RegisterAccount Command { get; set; }

        private readonly IAccountApplication _accountApplication ;
        private readonly IRoleAplication _roleAplication;

        public CreateModel(IAccountApplication accountApplication, IRoleAplication roleAplication)
        {
            _accountApplication = accountApplication;
            _roleAplication = roleAplication;
        }

        public void OnGet()
        {
            Roles = new SelectList(_roleAplication.List(), "Id", "Name");
        }

        public  void  OnPostCreate(RegisterAccount command)

        {


                _accountApplication.Register(command);



                if (OperationResult.IsSuccedded)

                    SuccessMessageame = OperationResult.Message;

                else

                    ErrorMessageame = OperationResult.Message;
        }



        

    }
}
