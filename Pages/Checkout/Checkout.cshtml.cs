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
    public class CheckoutModel : PageModel
    {
        private readonly GreensAndSipsContext _db;
        private readonly UserManager<IdentityUser> _UserManager;

        public IList<CheckoutItem> Items { get; private set; } = new List<CheckoutItem>(); // ✅ Prevents null issues
        public decimal Total { get; private set; }
        public long AmountPayable { get; private set; }

        public CheckoutModel(GreensAndSipsContext db, UserManager<IdentityUser> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            var customer = await _db.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null)
            {
                Console.WriteLine("❌ ERROR: Customer not found.");
                return NotFound("Customer not found.");
            }

            var basketItems = await _db.BasketItems
                .Where(b => b.BasketID == customer.BasketID)
                .Include(b => b.FoodItem)
                .ToListAsync();

            if (basketItems.Count == 0)
            {
                Console.WriteLine("❌ ERROR: Basket is empty!");
                return Page();
            }

            Items = basketItems.Select(b => new CheckoutItem
            {
                ID = b.StockID,
                Item_Name = b.FoodItem.ItemName,
                Price = b.FoodItem.Price,
                Quantity = b.Quantity
            }).ToList();

            Total = Items.Sum(i => i.Price * i.Quantity);
            AmountPayable = (long)Total;

            Console.WriteLine($"✅ Checkout Loaded! {Items.Count} items found.");

            return Page();
        }


    }
}
