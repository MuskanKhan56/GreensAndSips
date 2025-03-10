using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreensAndSips.Data;
using GreensAndSips.Models;

namespace GreensAndSips.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly GreensAndSipsContext _context;

        public CreateModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = new FoodItem();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
