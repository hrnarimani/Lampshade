using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
       

        public ArticleSearchModel SearchModel;
        public List<ArticleViewModel> Articles;
        public SelectList ArticleCategoeies;

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }


        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategoeies = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _articleApplication.Search(searchModel);
        }
    }
}
