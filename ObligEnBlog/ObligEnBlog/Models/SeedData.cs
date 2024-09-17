using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models {
    public static class SeedData {
        public static async void Initialize(IServiceProvider serviceProvider) {

            using (var context = new ObligEnBlogContext(serviceProvider.GetRequiredService<DbContextOptions<ObligEnBlogContext>>())) {

                //context.Blog.RemoveRange(await context.Blog.ToArrayAsync());
                //context.BlogPost.RemoveRange(await context.BlogPost.ToArrayAsync());


                if (context.Blog.Any() && context.BlogPost.Any()) {
                    Console.WriteLine("Hello******************************* *************************");
                    return;
                }

                var blogList = new Blog[] { new Blog { Name = "Best blog", Description = "the best blog created. period." }, new Blog { Name = "awesome blog", Description = "for all things awesome." }, new Blog { Name = "just a blog", Description = "we deal in mediocraty" } };
                Console.WriteLine(context.Blog);

                if (!context.Blog.Any()) { context.Blog.AddRange(blogList); Console.WriteLine("seeded Post"); }

                Console.WriteLine("***************************** *************** ****************************");

                if (!context.BlogPost.Any()) {
                    context.BlogPost.AddRange(new BlogPost { BlogParent = blogList[0], Title = "What does best mean?", Content = "Simply the best", Description = "a short blog about what the best means" }, new BlogPost { BlogParent = blogList[1], Title = "Awesome. Yes?", Content = "Are questions awesome or not? We the awesome blog say yes.", Description = "a short blog about what the best means" });
                }
                context.SaveChanges();

            }
        }
    }
}
