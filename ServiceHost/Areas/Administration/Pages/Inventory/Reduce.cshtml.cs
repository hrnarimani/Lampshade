using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class ReduceModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        public IncreaseInventory Command;

        private static long invt;




        private readonly IInventoryApplication _inventoryApplication;

        public ReduceModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
            invt = id;


        }
        [NeedsPermission(InventoryPermission.Reduce)]
        public IActionResult  OnPostReduce(ReduceInventory command)
        {
             command.InventoryId = invt;
            _inventoryApplication.Reduce(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
