using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Api.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactFormModel? Form { get; set; }

        public string? StatusMessage { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Contact Me";
        }

        public void OnPost()
        {
            ViewData["Title"] = "Contact Me";
            if (!ModelState.IsValid || Form == null)
            {
                StatusMessage = "Error: Please correct the errors below and try again.";
                return;
            }

            // Basic validation example (can be more sophisticated with data annotations on ContactFormModel)
            if (string.IsNullOrEmpty(Form.Name) ||
                string.IsNullOrEmpty(Form.Email) ||
                string.IsNullOrEmpty(Form.Subject) ||
                string.IsNullOrEmpty(Form.Message))
            {
                StatusMessage = "Error: All fields are required.";
                return;
            }

            // Here you would typically:
            // 1. Validate the input further.
            // 2. Send an email, save to a database, etc.
            // For now, just set a status message.
            StatusMessage = $"Thank you for your message, {Form.Name}! Your message regarding '{Form.Subject}' has been received (This is a placeholder - no email has been sent.)";

            // Clear the form after successful "submission"
            // Form = new ContactFormModel(); // This would require Form to be nullable or have a parameterless constructor
            ModelState.Clear(); // A simpler way to clear the form state for this placeholder
            Form = null; // Set form to null to clear fields
        }
    }

    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MinLength(5, ErrorMessage = "Message must be at least 5 characters long.")]
        public string? Message { get; set; }
    }
}
