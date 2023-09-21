using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LamphadeQuery.Contracts.ProductCategory
{
    public  interface  IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoriesWithProductsBy (string slug);
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
