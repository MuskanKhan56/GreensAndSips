using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreensAndSips.Pages.Contact
{
    // Handles the Contact Us page functionality
    public class IndexModel : PageModel
    {
        [BindProperty] // ✅ Binds form data to this property
        public ContactFormModel ContactForm { get; set; } = new ContactFormModel();

        public string MessageSent { get; private set; } = ""; // ✅ Stores the success message

        // Handles GET request (when the page loads initially)
        public void OnGet()
        {
        }

        // Handles POST request (when the form is submitted)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // ✅ Validate user input
            {
                return Page(); // ✅ If invalid, reload the page with errors
            }

            // ✅ Simulate email sending (Replace this with actual email logic later)
            MessageSent = "Thank you! Your message has been sent.";

            // ✅ Pass the success message to the page
            TempData["Message"] = MessageSent;
            return RedirectToPage(); // ✅ Redirect to refresh the page and display the message
        }

        // ✅ Contact Form Model - Defines the structure of the form inputs
        public class ContactFormModel
        {
            [Required, StringLength(100)]
            public string Name { get; set; } // ✅ User's name

            [Required, EmailAddress]
            public string Email { get; set; } // ✅ User's email address

            [Required, StringLength(500)]
            public string Message { get; set; } // ✅ User's message
        }
    }
}
