using _0_Framework.Application;
using CommentManagementApplication.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment 
{
    public class IndexModel : PageModel
    {

        
        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }
     
        public CommentSearchModel SearchModel;
        public List<CommentViewModel> Comments;

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }


        public void OnGet(CommentSearchModel searchModel)
        {
            
            Comments = _commentApplication.Search(searchModel);
        }



        public IActionResult OnGetCancel(long id)
        {
            _commentApplication.Cancel(id);



            if (OperationResult.IsSuccedded)
            {

                SuccessMessageameEd = OperationResult.Message;
                return RedirectToPage("./Index");
            }

            else

                ErrorMessageameEd = OperationResult.Message;

                return RedirectToPage("./Index");

        }


        public IActionResult OnGetConfirm(long  id)
        {
            _commentApplication.Confirm(id);



            if (OperationResult.IsSuccedded)
            {

                SuccessMessageameEd = OperationResult.Message;
                return RedirectToPage("./Index");
            }

            else

                ErrorMessageameEd = OperationResult.Message;

               return RedirectToPage("./Index");
        }
    }
}
