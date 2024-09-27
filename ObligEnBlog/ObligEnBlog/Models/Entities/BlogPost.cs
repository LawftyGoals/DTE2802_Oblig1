using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ObligEnBlog.Models.Entities {
    public class BlogPost {
        public int BlogPostId { get; set; }
        public int BlogParentId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }

    }
}
