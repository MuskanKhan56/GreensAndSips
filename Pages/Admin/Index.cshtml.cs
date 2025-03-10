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
    [Authorize(Roles = "Admin")] // 🔒 Restricts access to Admins only
    public class IndexModel : PageModel
    {
        private readonly GreensAndSipsContext _context; // Database context

        // Constructor to initialize the database context
        public IndexModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        // Stores the list of food items
        public List<FoodItem> FoodItems { get; set; } = new();

        // Handles GET request to display food items
        public async Task<IActionResult> OnGetAsync()
        {
            // ✅ Check if the database context is null
            if (_context.FoodItems == null)
            {
                return NotFound(); // Return a 404 response if FoodItems table doesn't exist
            }

            // ✅ Retrieve the list of food items asynchronously
            FoodItems = await _context.FoodItems.ToListAsync();

            return Page(); // Return the page with the data
        }
    }
}
