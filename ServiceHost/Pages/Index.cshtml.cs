using _0_Framework.Application.Email;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }


        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailService _emailService;  // just for test send email


    

        public void OnGet()
        {
            //_emailService.SendEmail("salam" , "hamid", "oldoldsa0001@gmail.com");
        }
    }
}