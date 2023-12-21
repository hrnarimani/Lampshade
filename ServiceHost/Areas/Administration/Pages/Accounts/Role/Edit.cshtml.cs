using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
       

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        
        public List<SelectListItem> Permissions = new List<SelectListItem>();
        public EditRole Command {get; set; }

        private readonly IRoleAplication _roleAplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;


        public EditModel(IRoleAplication roleAplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleAplication = roleAplication;
            _exposers = exposers;
        }


        public void OnGet(long id)
        {

            Command = _roleAplication.GetDetails(id);
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if(Command.MappedPermissions.Any(x=> x.Code == permission.Code))
                            item.Selected =true;

                        Permissions.Add(item);

                    }
                }
            }
            

        }

        public IActionResult  OnPostEdit(EditRole command)
        {
            _roleAplication.Edit(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
