using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreensAndSips.Pages.Menu
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly GreensAndSips.Data.GreensAndSipsContext _context;

        public EditModel(GreensAndSips.Data.GreensAndSipsContext context)
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

            var fooditem =  await _context.FoodItems.FirstOrDefaultAsync(m => m.ID == id);
            if (fooditem == null)
            {
                return NotFound();
            }
            FoodItem = fooditem;
            return Page();
        }

      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(FoodItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodItemExists(int id)
        {
          return (_context.FoodItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
