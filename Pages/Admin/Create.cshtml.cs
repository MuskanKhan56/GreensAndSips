using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreensAndSips.Data;
using GreensAndSips.Models;

namespace GreensAndSips.Pages.Admin
{
    // Handles the creation of a new FoodItem in the Admin panel
    public class CreateModel : PageModel
    {
        private readonly GreensAndSipsContext _context; // Database context

        // Constructor to initialize the database context
        public CreateModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        [BindProperty] // ✅ Binds form data to this property
        public FoodItem FoodItem { get; set; } = new FoodItem(); // Represents the food item being created

        // Handles GET request to display the create page
        public IActionResult OnGet()
        {
            return Page();
        }

        // Handles POST request to create a new FoodItem
        public async Task<IActionResult> OnPostAsync()
        {
            // ✅ Check if the model is valid
            if (!ModelState.IsValid)
            {
                return Page(); // Return the page if validation fails
            }

            // ✅ Add the new FoodItem to the database
            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync(); // Save changes asynchronously

            return RedirectToPage("./Index"); // Redirect to the list of food items after creation
        }
    }
}
