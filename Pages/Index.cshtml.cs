using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Data;
using GreensAndSips.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace GreensAndSips.Pages
{
   
    
    //[Authorize(Roles = "Admin, Member")]
    public class IndexModel : PageModel
    {
        private readonly GreensAndSipsContext _context;

        public IndexModel(GreensAndSipsContext context)
        {
            _context = context;
        }
       
        public IList<FoodItem> FoodItem { get; set; } = new List<FoodItem>();

        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null)
            {
                FoodItem = await _context.FoodItems.ToListAsync();
            }
        }
    }
}
