using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Configuration.Permission;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    [Authorize(Roles = RolesConst.Administrator)]//admin 
    public class IndexModel : PageModel
    {
      
        public InventorySearchModel  SearchModel;
        public List<InventoryViewMOdel> Inventory;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication ;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }


        [NeedsPermission(InventoryPermission.ListInventory)]
        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Seearch(searchModel);
        }

       
        
    }
}
