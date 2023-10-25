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
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

       
        public EditAccount Command {get; set; }
        public SelectList Roles;
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleAplication _roleAplication;

        public EditModel(IAccountApplication accountApplication, IRoleAplication roleAplication)
        {
            _accountApplication = accountApplication;
            _roleAplication = roleAplication;
        }

        public void OnGet(long id)
        {

            Command = _accountApplication.Getdetails(id);
            Roles = new SelectList(_roleAplication.List(), "Id", "Name");

        }

        public IActionResult  OnPostEdit(EditAccount command)
        {
            _accountApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
