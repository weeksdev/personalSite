using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.Api.Data;
using PersonalSite.Api.Models;
using System;
using System.Threading.Tasks;

namespace PersonalSite.Api.Pages.Admin.Blog
{
    public class CreateModel : PageModel
    {
        private readonly BlogDbContext _context;

        public CreateModel(BlogDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; } = new Post(); // Initialize to avoid null reference issues in the form

        public IActionResult OnGet()
        {
            // Pre-populate if needed, for example, with a default PublishedDate
            Post.PublishedDate = DateTime.UtcNow;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.Posts == null)
            {
                return Page(); // Or handle more gracefully
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
