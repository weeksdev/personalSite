using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Api.Data;
using PersonalSite.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Api.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _context;

        public IndexModel(BlogDbContext context)
        {
            _context = context;
        }

        public IList<Post> Posts { get; set; } = new List<Post>();

        public async Task OnGetAsync()
        {
            if (_context.Posts != null)
            {
                Posts = await _context.Posts
                                    .OrderByDescending(p => p.PublishedDate)
                                    .ToListAsync();
            }
        }
    }
}
