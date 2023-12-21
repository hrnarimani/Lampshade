using System.Collections.Generic;
using _0_Framework.Infrastructure;
using ShopManagement.Configuration.Permission;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ListProducts, "ListProducts"),
                        new PermissionDto(ShopPermission.SearchProducts, "SearchProducts"),
                        new PermissionDto(ShopPermission.CreateProduct, "CreateProduct"),
                        new PermissionDto(ShopPermission.EditProduct, "EditProduct"),

                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.SearchProductCategories, "SearchProductCategories"),
                        new PermissionDto(ShopPermission.ListProductCategories, "ListProductCategories"),
                        new PermissionDto(ShopPermission.CreateProductCategory, "CreateProductCategory"),
                        new PermissionDto(ShopPermission.EditProductCategory, "EditProductCategory"),
                }   }
            };
        }
    }
}






