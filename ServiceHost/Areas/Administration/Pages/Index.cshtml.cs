using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AccountManagement.Application.Contracts.Account;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
     
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public Chart DoughnutDataSet { get; set; }
        public List<Chart> BarLineDataSet { get; set; }

       


        public void OnGet()
        {

            var prices = _inventoryApplication.GetPrices();
            var doublePrice = new List<double>();
           
            foreach (var price in prices)
            {

                doublePrice.Add(price.UnitPrice);
            }



            BarLineDataSet = new List<Chart>
            {
                new Chart
                {
                    Label = "Apple",
                    Data =doublePrice, // javabe query bala 
                    BackgroundColor = new[] {"#ffcdb2"},
                    BorderColor = "#b5838d"
                },
                new Chart
                {
                    Label = "Samsung",
                    Data = new List<double> {200, 300, 350, 270, 100},
                    BackgroundColor = new[] {"#ffc8dd"},
                    BorderColor = "#ffafcc"
                },
                new Chart
                {
                    Label = "Total",
                    Data = new List<double> {300, 500, 600, 440, 150},
                    BackgroundColor = new[] {"#0077b6"},
                    BorderColor = "#023e8a"
                },
            };
            DoughnutDataSet = new Chart
            {
                Label = "Apple",
                Data = new List<double> { 100, 200, 250, 170, 50 },
                BorderColor = "#ffcdb2",
                BackgroundColor = new[] { "#b5838d", "#ffd166", "#7f4f24", "#ef233c", "#003049" }
            };
        }
    }

   
}