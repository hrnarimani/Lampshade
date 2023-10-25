using _0_Framework.Application;
using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class ChangePasswordModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        private static  long intvc;

        public ChangePassword Command { get; set; }
        public SelectList Roles;
        private readonly IAccountApplication _accountApplication;

        public ChangePasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(long id)
        {

            intvc = id;
        }
        public IActionResult OnPostChangePassword(ChangePassword  command)
        {
           _accountApplication.ChangePassword(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");
        }
    }
}
