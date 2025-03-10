using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GreensAndSips.Pages.Menu
{
    // Handles displaying the menu and adding items to the user's basket
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager; // ✅ Manages user authentication
        private readonly GreensAndSipsContext _db; // ✅ Database context

        // Constructor to initialize the database and user manager
        public IndexModel(GreensAndSipsContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Stores the list of food items to be displayed on the menu page
        public IList<FoodItem> FoodItem { get; set; } = new List<FoodItem>();

        // Handles GET request to load food items from the database
        public async Task OnGetAsync()
        {
            if (_db.FoodItems != null) // ✅ Check if FoodItems table exists
            {
                FoodItem = await _db.FoodItems.ToListAsync(); // ✅ Retrieve food items from database
            }
        }

        /// <summary>
        /// Adds an item to the user's basket when they click "Buy"
        /// </summary>
        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User); // ✅ Get the logged-in user
            if (user == null) return RedirectToPage(); // ✅ Redirect if user is not logged in

            // ✅ Retrieve the customer's basket using their email
            CheckoutCustomer? customer = await _db.CheckoutCustomers
                .FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null) return RedirectToPage(); // ✅ Redirect if customer not found

            // ✅ Check if the item is already in the basket
            BasketItem? item = await _db.BasketItems
                .FirstOrDefaultAsync(b => b.StockID == itemID && b.BasketID == customer.BasketID);

            if (item == null)
            {
                // ✅ Add a new item to the basket if not already present
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };

                _db.BasketItems.Add(newItem);
            }
            else
            {
                // ✅ Increase the quantity if the item already exists in the basket
                item.Quantity += 1;
                _db.BasketItems.Update(item);
            }

            await _db.SaveChangesAsync(); // ✅ Save changes to the database
            return RedirectToPage(); // ✅ Refresh the page to reflect updates
        }
    }
}
