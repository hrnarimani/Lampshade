using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permission;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [Authorize(Roles = RolesConst.Administrator)]//admin && content uploader
    
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;


        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        [NeedsPermission(ShopPermission.ListProductCategories )]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }
    }
}
