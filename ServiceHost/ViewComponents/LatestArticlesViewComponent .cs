using _01_LamphadeQuery.Contracts.Article;
using _01_LamphadeQuery.Contracts.Product;

using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent:ViewComponent
    {
        private readonly IArticleQuery  _articleQuery;

        public LatestArticlesViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }


        IViewComponentResult Invoke()
        {
            var articles = _articleQuery.LatestArticles();
            return View(articles);
        }
    }
}
