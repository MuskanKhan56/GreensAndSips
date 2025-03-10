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
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GreensAndSipsContext _db;

        public IndexModel(GreensAndSipsContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IList<FoodItem> FoodItem { get; set; } = new List<FoodItem>();

        public async Task OnGetAsync()
        {
            if (_db.FoodItems != null)
            {
                FoodItem = await _db.FoodItems.ToListAsync();
            }
        }

        /// <summary>
        /// Adds an item to the user's basket when they click "Buy"
        /// </summary>
        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage();

            CheckoutCustomer? customer = await _db.CheckoutCustomers
                .FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null) return RedirectToPage();

            // 🔹 Query basket item correctly
            BasketItem? item = await _db.BasketItems
                .FirstOrDefaultAsync(b => b.StockID == itemID && b.BasketID == customer.BasketID);

            if (item == null)
            {
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
                item.Quantity += 1;
                _db.BasketItems.Update(item);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }






    }
}
