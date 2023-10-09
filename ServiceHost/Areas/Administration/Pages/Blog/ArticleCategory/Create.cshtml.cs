using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }
        
      

        public CreateArticleCategory Command { get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }


        public void OnGet()
        {

        }

        public void OnPostCreate(CreateArticleCategory command)

        {
           
                _articleCategoryApplication.Create(command);



                if (OperationResult.IsSuccedded)

                    SuccessMessageame = OperationResult.Message;

                else

                    ErrorMessageame = OperationResult.Message;
            

           
        }

    }
}
