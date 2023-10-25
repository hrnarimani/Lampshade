using System.Data;
using _01_LamphadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Infrastructure.EF.Core;
using CommentManagementApplication.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            Product = _productQuery.GetProductDetails( id);
        }
        
        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentTypes.Product;
            var result= _commentApplication.Add(command);

            return RedirectToPage("/Product", new { Id = productSlug });
        }

       
        
    }
}
