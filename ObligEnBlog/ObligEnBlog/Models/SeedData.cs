using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models {
    public static class SeedData {
        public static async void Initialize(IServiceProvider serviceProvider) {

            using (var context = new ObligEnBlogContext(serviceProvider.GetRequiredService<DbContextOptions<ObligEnBlogContext>>())) {

                if (context.Blog.Any() && context.BlogPost.Any()) {
                    return;
                }
                //context.Blog.RemoveRange(context.Blog.ToArray());
                //context.BlogPost.RemoveRange(context.BlogPost.ToArray());

                var blogList = new Blog[] { new Blog { Name = "Best blog", Description = "the best blog created. period." }, new Blog { Name = "awesome blog", Description = "for all things awesome." }, new Blog { Name = "just a blog", Description = "we deal in mediocraty" } };


                if (!context.Blog.Any()) { await context.Blog.AddRangeAsync(blogList); Console.WriteLine("seeded Blog"); }

                if (!context.BlogPost.Any()) {
                    await context.BlogPost.AddRangeAsync(new BlogPost { BlogParentId = 1, Title = "What does best mean?", Content = "Simply the best", Description = "a short blog about what the best means" }, new BlogPost { BlogParentId = 0, Title = "Awesome. Yes?", Content = "Are questions awesome or not? We the awesome blog say yes.", Description = "a short blog about what the best means" });
                    Console.WriteLine("seeded BlogPost");
                }
                await context.SaveChangesAsync();

            }
        }
    }
}
