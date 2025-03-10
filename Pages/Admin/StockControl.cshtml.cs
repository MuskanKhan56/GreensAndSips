using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GreensAndSips.Pages.Admin
{
    public class StockControlModel : PageModel
    {
        private readonly GreensAndSipsContext _db;

        public StockControlModel(GreensAndSipsContext db)
        {
            _db = db;
        }

        public IList<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

        public async Task OnGetAsync()
        {
            FoodItems = await _db.FoodItems.ToListAsync();
        }

        [BindProperty]
        public int UpdatedStock { get; set; }

        public async Task<IActionResult> OnPostUpdateStockAsync(int itemID)
        {
            var foodItem = await _db.FoodItems.FindAsync(itemID);
            if (foodItem != null)
            {
                foodItem.Stock = UpdatedStock;
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
