using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Api.Data;
using PersonalSite.Api.Models;
using System.Threading.Tasks;

namespace PersonalSite.Api.Pages.Blog
{
    public class PostModel : PageModel
    {
        private readonly BlogDbContext _context;

        public PostModel(BlogDbContext context)
        {
            _context = context;
        }

        public Post? Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
