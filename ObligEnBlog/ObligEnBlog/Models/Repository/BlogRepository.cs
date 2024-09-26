using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;
using System.Reflection.Metadata;

namespace ObligEnBlog.Models.Repository;

public class BlogRepository : IBlogRepository, IDisposable
{
    private ObligEnBlogContext context;

    public BlogRepository(ObligEnBlogContext context)
    {
        this.context = context;
    }

    public IEnumerable<Blog> GetBlogs()
    {
        return context.Blog.ToList();
    }

    public Blog GetBlogById(int BlogId)
    {
        return context.Blog.Find(BlogId);
    }
    public void AddBlog(Blog blog)
    {
        context.Blog.Add(blog);
    }
    public void DeleteBlog(int blogId)
    {
        var blog = context.Blog.Find(blogId);
        context.Blog.Remove(blog);
    }
    public void UpdateBlog(Blog blog)
    {
        context.Entry(blog).State = EntityState.Modified;
    }


    public IEnumerable<BlogPost> GetAllBlogPosts()
    {
        return context.BlogPost.ToList();
    }
    public BlogPost GetBlogPostById(int blogPostId)
    {
        return context.BlogPost.Find(blogPostId);
    }
    public void AddBlogPost(BlogPost blogPost)
    {
        context.BlogPost.Add(blogPost);
    }
    public void DeleteBlogPost(int blogPostId)
    {
        var blogPost = context.BlogPost.Find(blogPostId);
        context.BlogPost.Remove(blogPost);
    }
    public void UpdateBlogPost(BlogPost blogPost)
    {
        context.Entry(blogPost).State = EntityState.Modified;
    }


    public IEnumerable<Comment> GetAllComments()
    {
        return context.Comment.ToList();
    }
    public Comment GetCommentById(int commentId)
    {
        return context.Comment.Find(commentId);
    }
    public void AddComment(Comment comment)
    {
        context.Comment.Add(comment);
    }
    public void DeleteComment(int commentId)
    {
        var comment = context.Comment.Find(commentId);
        context.Comment.Remove(comment);
    }
    public void UpdateComment(Comment comment)
    {
        context.Entry(comment).State = EntityState.Modified;
    }


    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
