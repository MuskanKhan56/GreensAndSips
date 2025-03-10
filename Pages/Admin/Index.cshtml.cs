using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using GreensAndSips.Data;
using GreensAndSips.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreensAndSips.Pages.Admin
{
    [Authorize(Roles = "Admin")] // 🔒 Only Admins can access
    public class IndexModel : PageModel
    {
        private readonly GreensAndSipsContext _context;

        public IndexModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        public List<FoodItem> FoodItems { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }

            FoodItems = await _context.FoodItems.ToListAsync();
            return Page();
        }
    }
}
