using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ActiveUserModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAuthHelper _authHelper;

        public ActiveUserModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public ActiveViewModel Command { get; set; }

        [TempData] public string ErrorMessageame { get; set; }

        [TempData] public string SuccessfuleMessage { get; set; }




        public void OnGet()
        {
            
        }

        public IActionResult  OnPostConfirm(ActiveViewModel command)
        {
           
            
                if (_accountApplication.ActiveUser(command.Code))
                {
                    SuccessfuleMessage = "ثبت نام با موفقیت انجام شد لطفا وارد شوید"; 
                    return RedirectToPage("./Account");

                }

                else
                {
                    
                ErrorMessageame = "کدوارد شده صحیح نمی باشد";
                return Page();
                }
               
               
        }
    }
}