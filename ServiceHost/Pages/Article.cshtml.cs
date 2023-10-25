using _01_LamphadeQuery.Contracts.Article;
using _01_LamphadeQuery.Contracts.ArticleCategory;
using BlogManagement.Application.Contracts.Article;
using CommentManagement.Infrastructure.EF.Core;
using CommentManagementApplication.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {



        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;

        private readonly ICommentApplication _commentApplication;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleModel(ICommentApplication commentApplication, IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _commentApplication = commentApplication;
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }


        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type=CommentTypes.ArticleS;
            var result= _commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
