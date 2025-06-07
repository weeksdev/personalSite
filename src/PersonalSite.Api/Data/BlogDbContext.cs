using Microsoft.EntityFrameworkCore;
using PersonalSite.Api.Models;

namespace PersonalSite.Api.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // You can add any model configuration here if needed in the future
            // For example, setting up indexes or relationships
            modelBuilder.Entity<Post>().ToTable("BlogPosts");
        }
    }
}
