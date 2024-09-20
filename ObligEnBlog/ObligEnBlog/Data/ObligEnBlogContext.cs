﻿using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Data {
    public class ObligEnBlogContext : DbContext {
        public ObligEnBlogContext(DbContextOptions<ObligEnBlogContext> options)
            : base(options) {
        }

        public DbSet<Blog> Blog { get; set; } = default!;



        public DbSet<BlogPost> BlogPost { get; set; }



        public DbSet<Comment> Comment { get; set; }

    }
}
