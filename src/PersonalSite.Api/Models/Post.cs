using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Api.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        [StringLength(100)]
        public string? Author { get; set; } // Optional: Or you might want a separate User model later
    }
}
