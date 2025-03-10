using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreensAndSips.Pages.Contact
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ContactFormModel ContactForm { get; set; } = new ContactFormModel();

        public string MessageSent { get; private set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ✅ Simulate email sending (Replace this with actual email logic later)
            MessageSent = "Thank you! Your message has been sent.";

            // ✅ Pass the success message to the page
            TempData["Message"] = MessageSent;
            return RedirectToPage();
        }

        public class ContactFormModel
        {
            [Required, StringLength(100)]
            public string Name { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, StringLength(500)]
            public string Message { get; set; }
        }
    }
}
