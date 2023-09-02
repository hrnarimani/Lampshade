using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides 
{
    public class CreateModel : PageModel
    {
        

        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        
		public CreateSlide Command { get; set; }

        private readonly ISlideApplication _slideApplication;

        public CreateModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }


        public void OnGet()
        {
            
        }

        public  IActionResult  OnPostCreate(CreateSlide  command)

        {

            _slideApplication.Create(command);
            
          if (OperationResult.IsSuccedded)
            {
                SuccessMessageame = OperationResult.Message;
                return RedirectToPage("./Create");
            }

               
            else
            {
                ErrorMessageame = OperationResult.Message;
                return RedirectToPage("./Create");

            }
                



        }

    }
}
