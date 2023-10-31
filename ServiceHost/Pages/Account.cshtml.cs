using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {

        [TempData]
        public string LoginMessage { get; set; }

        [TempData]
        public string RegisterMessage { get; set; }

        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin (Login command)
        {
            _accountApplication.Login(command);

            if (OperationResult.IsSuccedded)
                return RedirectToPage("./Index");

            LoginMessage = OperationResult.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetLogout()
        {

            _accountApplication.Logout();
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostRegister(RegisterAccount command)
        {

            _accountApplication.Register(command);
            if (OperationResult.IsSuccedded)

                return RedirectToPage("/Account");


              RegisterMessage = OperationResult.Message;

            return RedirectToPage("/Account");
        }
    }
}
