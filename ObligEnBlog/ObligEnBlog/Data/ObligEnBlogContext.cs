using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Data;
public class ObligEnBlogContext : IdentityDbContext<IdentityUser> {
    public ObligEnBlogContext(DbContextOptions<ObligEnBlogContext> options)
        : base(options) { }

    public DbSet<Blog> Blog { get; set; } = default!;
    public DbSet<BlogPost> BlogPost { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<IdentityUser> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

    }
}
