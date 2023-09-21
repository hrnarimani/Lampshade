using _01_LamphadeQuery.Contracts.Product;

using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent:ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        IViewComponentResult Invoke()
        {
            var products = _productQuery.GetLatestArrivals ();
            return View(products);
        }
    }
}
