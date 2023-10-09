using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
       

        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }
        
      

        public CreateArticle Command { get; set; }
        public SelectList ArticleCategoeies;

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }


        public void OnGet()
        {
            ArticleCategoeies = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public void OnPostCreate(CreateArticle command)

        {
           
                _articleApplication.Create(command);



                if (OperationResult.IsSuccedded)

                    SuccessMessageame = OperationResult.Message;

                else

                    ErrorMessageame = OperationResult.Message;
            

           
        }

    }
}
