using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var trans = default(IDbContextTransaction);
            try
            {
                using (var context = new ObligEnBlogContext(serviceProvider.GetRequiredService<DbContextOptions<ObligEnBlogContext>>()))
                {
                    trans = await context.Database.BeginTransactionAsync();
                    context.Blog.RemoveRange(await context.Blog.ToArrayAsync());
                    context.BlogPost.RemoveRange(await context.BlogPost.ToArrayAsync());
                    

                    if (context.Blog.Any() && context.BlogPost.Any())
                    {
                        Console.WriteLine("Hello******************************* *************************");
                        return;
                    }

                    var blogList = new Blog[] { new Blog { Name = "Best blog", Description = "the best blog created. period." }, new Blog { Name = "awesome blog", Description = "for all things awesome." }, new Blog { Name = "just a blog", Description = "we deal in mediocraty" } };


                    if (!context.Blog.Any()) { context.Blog.AddRange(blogList); }

                    Console.WriteLine("***************************** *************** ****************************");

                    if (!context.BlogPost.Any())
                    {
                        context.BlogPost.AddRange(new BlogPost
                        { BlogParent = await context.Blog.FirstAsync(m => m.BlogId == 0), Title = "What does best mean?", Content = "Simply the best", Description = "a short blog about what the best means" }, new BlogPost { BlogParent = await context.Blog.FirstAsync(m => m.BlogId == 1), Title = "Awesome. Yes?", Content = "Are questions awesome or not? We the awesome blog say yes.", Description = "a short blog about what the best means" });
                    }

                    await context.SaveChangesAsync();
                    await trans.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                if (trans != null)
                { await trans.RollbackAsync(); }
                Console.WriteLine(ex.Message);
            }
            finally { if (trans != null) { await trans.DisposeAsync(); } }
        }
    }
}
