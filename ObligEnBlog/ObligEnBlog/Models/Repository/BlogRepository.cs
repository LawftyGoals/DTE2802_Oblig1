using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;
using System.Security.Principal;

namespace ObligEnBlog.Models.Repository;

public class BlogRepository : IBlogRepository, IDisposable {
    private ObligEnBlogContext context;
    private readonly UserManager<IdentityUser> _manager;

    public BlogRepository(ObligEnBlogContext context, UserManager<IdentityUser> userManager) {
        this.context = context;
        this._manager = userManager;
    }

    public IEnumerable<Blog> GetAllBlogs() {
        return context.Blog.ToList();
    }

    public Blog? GetBlogById(int? BlogId) {
        if (BlogId == null) return null;
        return context.Blog.Find(BlogId);
    }
    public void AddBlog(Blog blog, IPrincipal principal) {
        var user = _manager.FindByNameAsync(principal.Identity.Name).Result;
        blog.Owner = user;
        blog.OwnerId = user.Id;

        context.Blog.Add(blog);
    }
    public void DeleteBlog(int blogId) {
        var blog = context.Blog.Find(blogId);
        if (blog == null) return;
        context.Blog.Remove(blog);
    }
    public void UpdateBlog(Blog blog) {
        context.Entry(blog).State = EntityState.Modified;
    }


    public IEnumerable<BlogPost> GetAllBlogPosts() {
        var blogPosts = context.BlogPost.Include(bp => bp.Owner);
        return blogPosts.ToList();
    }
    public BlogPost? GetBlogPostById(int? blogPostId) {
        return context.BlogPost.Find(blogPostId);
    }
    public void AddBlogPost(BlogPost blogPost, IPrincipal principal) {
        var user = _manager.FindByNameAsync(principal.Identity.Name).Result;
        context.BlogPost.Add(blogPost);
    }
    public void DeleteBlogPost(int blogPostId) {
        var blogPost = context.BlogPost.Find(blogPostId);
        if (blogPost == null) return;
        context.BlogPost.Remove(blogPost);
    }
    public void DeleteBlogPosts(List<BlogPost> blogPosts) {
        if (blogPosts != null) {
            context.BlogPost.RemoveRange(blogPosts);
        }
    }
    public void UpdateBlogPost(BlogPost blogPost) {
        context.Entry(blogPost).State = EntityState.Modified;
    }


    public IEnumerable<Comment> GetAllComments() {
        var comments = context.Comment.Include(c => c.Owner);
        return comments.ToList();
    }
    public Comment? GetCommentById(int? commentId) {
        return context.Comment.Find(commentId);
    }
    public void AddComment(Comment comment, IPrincipal principal) {
        var user = _manager.FindByNameAsync(principal.Identity.Name).Result;
        context.Comment.Add(comment);
    }
    public void DeleteComment(int commentId) {
        var comment = context.Comment.Find(commentId);
        if (comment == null) return;
        context.Comment.Remove(comment);
    }
    public void DeleteComments(List<Comment> comments) {
        if (comments != null) {
            context.Comment.RemoveRange(comments);
        }
    }
    public void UpdateComment(Comment comment) {
        context.Entry(comment).State = EntityState.Modified;
    }

    public IdentityUser? GetUser(string userId) {
        var user = context.User.Find(userId);
        return user;
    }


    public void Save() {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing) {
        if (!disposed) {
            if (disposing) {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
