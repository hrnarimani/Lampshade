using _0_Framework.Application;
using _01_LamphadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {

        [TempData]
        public string ErrorMessageame { get; set; }

        [TempData]
        public string SuccessMessageame { get; set; }


        public ProductQueryModel Product;

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

       


       

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails( id);
        }
        
        public IActionResult OnPost(AddComment command, string productSlug)
        {
            _commentApplication.Add(command);



            return RedirectToPage("/Product", new { Id = productSlug });
        }

       
        
    }
}
