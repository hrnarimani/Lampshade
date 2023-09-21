using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IncreaseModel : PageModel
    {

        [TempData]
        public string ErrorMessageameEd { get; set; }

        [TempData]
        public string SuccessMessageameEd { get; set; }

        public IncreaseInventory Command;

        private static long invt;




        private readonly IInventoryApplication _inventoryApplication;

        public IncreaseModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
              invt = id;
        }

        public IActionResult  OnPostIncrease(IncreaseInventory command)
        {
            command.InventoryId = invt;
            _inventoryApplication.Increase(command);



            if (OperationResult.IsSuccedded)

                SuccessMessageameEd = OperationResult.Message;

            else

                ErrorMessageameEd = OperationResult.Message;

            return RedirectToPage("./Index");

        }
    }
}
