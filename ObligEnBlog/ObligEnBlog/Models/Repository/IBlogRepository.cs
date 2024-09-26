using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models.Repository;


public interface IBlogRepository : IDisposable
{
    IEnumerable<Blog> GetBlogs();
    Blog GetBlogById(int BlogId);
    void AddBlog(Blog blog);
    void DeleteBlog(Blog blog);
    void UpdateBlog(Blog blog);


    IEnumerable<BlogPost> GetAllBlogPosts();
    BlogPost GetBlogPostById(int blogPostId);
    void AddBlogPost(BlogPost blogPost);
    void DeleteBlogPost(BlogPost blogPost);
    void UpdateBlogPost(BlogPost blogPost);


    IEnumerable<Comment> GetAllComments();
    Comment GetCommentById(int commentId);
    void AddComment(Comment comment);
    void DeleteComment(Comment comment);
    void UpdateComment(Comment comment);


    void Save();
}

