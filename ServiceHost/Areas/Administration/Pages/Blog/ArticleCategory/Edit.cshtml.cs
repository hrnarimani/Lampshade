using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class EditModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        public EditArticleCategory Command { get; set; }

        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }


        public void OnGet(long id)
        {
            Command = _articleCategoryApplication.GetDetails(id);
        }

        public IActionResult  OnPostEdit(EditArticleCategory command)
        {
            _articleCategoryApplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
