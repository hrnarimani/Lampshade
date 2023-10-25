using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role  
{
    public class IndexModel : PageModel
    {
      
        
        public List<RoleViewModel> Roles;
       

       
        private readonly IRoleAplication _roleAplication;

        public IndexModel( IRoleAplication roleAplication)
        {
            
            _roleAplication = roleAplication;
        }

        public void OnGet()
        {

            Roles = _roleAplication.List();
        }

    }




}


