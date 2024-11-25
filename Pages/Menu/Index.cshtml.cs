using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;

namespace GreensAndSips.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly GreensAndSips.Data.GreensAndSipsContext _context;

        public IndexModel(GreensAndSips.Data.GreensAndSipsContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null)
            {
                FoodItem = await _context.FoodItems.ToListAsync();
            }
        }
    }
}
