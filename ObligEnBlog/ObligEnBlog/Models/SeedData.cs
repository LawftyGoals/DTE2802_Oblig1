using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

// IF THINGS GO PEARSHAPED THERE ARE A BUNCH OF ASYNCS DOWN HERE THAT DO NOT NEED TO BE ASYNCED.

namespace ObligEnBlog.Models;
public static class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {

        using (var context = new ObligEnBlogContext(serviceProvider.GetRequiredService<DbContextOptions<ObligEnBlogContext>>()))
        {
            await RemoveAllEntities(context);

            if (context.Blog.Any() && context.BlogPost.Any())
            {
                return;
            }
            var blogList = new Blog[] { new Blog { Name = "Best blog", Description = "the best blog created. period." }, new Blog { Name = "awesome blog", Description = "for all things awesome." }, new Blog { Name = "just a blog", Description = "we deal in mediocraty" } } as IEnumerable<Blog>;

            if (!context.Blog.Any()) { blogList = await CreateBlogsAsync(blogList, context); Console.WriteLine("seeded Blog"); }

            var blogPostList = new BlogPost[] { new BlogPost { BlogParentId = blogList.ElementAt(0).BlogId, Title = "What does best mean?", Content = "Simply the best", Description = "a short blog about what the best means" }, new BlogPost { BlogParentId = blogList.ElementAt(1).BlogId, Title = "Awesome. Yes?", Content = "Are questions awesome or not? We the awesome blog say yes.", Description = "a short blog about what the best means" } } as IEnumerable<BlogPost>;

            if (!context.BlogPost.Any())
            {
                blogPostList = await CreateBlogPostsAsync(blogPostList, context);

                Console.WriteLine("seeded BlogPost");
            }
 

        }
    }

    private static async Task RemoveAllEntities(ObligEnBlogContext dbContext)
    {
        var transaction = default(IDbContextTransaction);
        try
        {
            transaction = await dbContext.Database.BeginTransactionAsync();
            dbContext.Blog.RemoveRange(dbContext.Blog);
            dbContext.BlogPost.RemoveRange(dbContext.BlogPost);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }

        }


    }


    private static async Task<IEnumerable<Blog>> CreateBlogsAsync(IEnumerable<Blog> blogs, ObligEnBlogContext dbContext)
    {
        var transaction = default(IDbContextTransaction);
        try
        {
            transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.Blog.AddRangeAsync(blogs);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }

        }

        return blogs;
    }
    private static async Task<IEnumerable<BlogPost>> CreateBlogPostsAsync(IEnumerable<BlogPost> blogPosts, ObligEnBlogContext dbContext)
    {
        var transaction = default(IDbContextTransaction);
        try
        {
            transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.BlogPost.AddRangeAsync(blogPosts);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }

        }

        return blogPosts;
    }
}
