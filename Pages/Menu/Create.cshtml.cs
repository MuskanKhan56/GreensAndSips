using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreensAndSips.Data;
using GreensAndSips.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreensAndSips.Pages.Menu
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly GreensAndSipsContext _context;

        public CreateModel(GreensAndSipsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = new FoodItem();

        [BindProperty]
        public IFormFile ImageFile { get; set; }  // ✅ Add this

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            foreach(var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                FoodItem.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }

            // ✅ Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    FoodItem.ImageData = memoryStream.ToArray();  // ✅ Convert image to binary
                }
            }

            try
            {
                _context.FoodItems.Add(FoodItem);
                await _context.SaveChangesAsync();
                Console.WriteLine($" Successfully added FoodItem: {FoodItem.ItemName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error saving data: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the food item.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
