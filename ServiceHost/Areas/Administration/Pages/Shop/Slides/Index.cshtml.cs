using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        
                    
        public List<SlideViewModel> Slides;

        private readonly ISlideApplication _slideApplication;


        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList(); // be jaye search omade
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (OperationResult.IsSuccedded)
            
               
                return RedirectToPage("./Index");
               
            else
            
                return RedirectToPage("./Index");
               
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore( id);
            if (OperationResult.IsSuccedded)
            
                return RedirectToPage("./Index");
                
            else
                return RedirectToPage("./Index");
               

            


        }



    }
}
