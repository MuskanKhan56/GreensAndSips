using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreensAndSips.Data;

namespace GreensAndSips.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel>logger)
        {
            _logger = logger;
        }
        private readonly GreensAndSipsContext _db;
        public void OnGet()
        {

        }
    }
}