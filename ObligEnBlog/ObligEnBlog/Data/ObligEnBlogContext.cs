using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;
using System.Reflection.Emit;

namespace ObligEnBlog.Data {
    public class ObligEnBlogContext : DbContext {
        public ObligEnBlogContext(DbContextOptions<ObligEnBlogContext> options)
            : base(options) {
        }

        public DbSet<Blog> Blog { get; set; } = default!;
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
