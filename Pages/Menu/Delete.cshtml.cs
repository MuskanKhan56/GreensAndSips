using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreensAndSips.Pages.Menu
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly GreensAndSipsContext _context;

        public DeleteModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var fooditem = await _context.FoodItems.FirstOrDefaultAsync(m => m.ID == id);

            if (fooditem == null)
            {
                return NotFound();
            }
            else
            {
                FoodItem = fooditem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var fooditem = await _context.FoodItems.FindAsync(id);

            if (fooditem != null)
            {
                // ✅ Step 1: Remove associated BasketItems
                var basketItems = _context.BasketItems.Where(b => b.StockID == id);
                if (basketItems.Any())
                {
                    _context.BasketItems.RemoveRange(basketItems);
                    await _context.SaveChangesAsync(); // Ensure BasketItems are deleted first
                }

                // ✅ Step 2: Now delete the FoodItem
                _context.FoodItems.Remove(fooditem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
