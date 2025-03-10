using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreensAndSips.Data;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreensAndSips.Pages.Checkout
{
    // Handles the checkout process for the user
    public class CheckoutModel : PageModel
    {
        private readonly GreensAndSipsContext _db; // Database context
        private readonly UserManager<IdentityUser> _UserManager; // User management service

        public IList<CheckoutItem> Items { get; private set; } = new List<CheckoutItem>(); // ✅ Prevents null issues
        public decimal Total { get; private set; } // ✅ Stores the total price of the order
        public long AmountPayable { get; private set; } // ✅ Stores the total price as a long (for payment processing)

        // Constructor to initialize dependencies
        public CheckoutModel(GreensAndSipsContext db, UserManager<IdentityUser> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        // Handles GET request to load the checkout page
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User); // ✅ Get the logged-in user
            if (user == null)
            {
                return RedirectToPage("/Identity/Account/Login"); // ✅ Redirect to login if not authenticated
            }

            // ✅ Retrieve the customer's basket using their email
            var customer = await _db.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null)
            {
                Console.WriteLine("❌ ERROR: Customer not found.");
                return NotFound("Customer not found.");
            }

            // ✅ Retrieve basket items and include related food item details
            var basketItems = await _db.BasketItems
                .Where(b => b.BasketID == customer.BasketID)
                .Include(b => b.FoodItem)
                .ToListAsync();

            if (basketItems.Count == 0)
            {
                Console.WriteLine("❌ ERROR: Basket is empty!");
                return Page(); // ✅ Show empty basket message
            }

            // ✅ Convert BasketItems to CheckoutItems for display
            Items = basketItems.Select(b => new CheckoutItem
            {
                ID = b.StockID,
                Item_Name = b.FoodItem.ItemName,
                Price = b.FoodItem.Price,
                Quantity = b.Quantity
            }).ToList();

            // ✅ Calculate the total cost of the items
            Total = Items.Sum(i => i.Price * i.Quantity);
            AmountPayable = (long)Total; // ✅ Convert total to long for potential payment processing

            Console.WriteLine($"✅ Checkout Loaded! {Items.Count} items found.");

            return Page(); // ✅ Return the checkout page with data
        }
    }
}
