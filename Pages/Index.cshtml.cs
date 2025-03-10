using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GreensAndSips.Pages
{
    // Handles the homepage logic for listing food items
    public class IndexModel : PageModel
    {
        private readonly GreensAndSipsContext _context; // Database context

        // Constructor to initialize the database context
        public IndexModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        // Stores the list of food items to be displayed on the homepage
        public IList<FoodItem> FoodItem { get; set; } = new List<FoodItem>();

        // Handles GET request to retrieve food items from the database
        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null) // Check if the FoodItems table exists
            {
                FoodItem = await _context.FoodItems.ToListAsync(); // Retrieve all food items
            }
        }
    }
}
