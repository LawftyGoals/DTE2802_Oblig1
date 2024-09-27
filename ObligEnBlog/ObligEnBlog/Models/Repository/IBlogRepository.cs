using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models.Repository;


public interface IBlogRepository : IDisposable
{
    IEnumerable<Blog> GetAllBlogs();
    Blog? GetBlogById(int? BlogId);
    void AddBlog(Blog blog);
    void DeleteBlog(int blogPostId);
    void UpdateBlog(Blog blog);


    IEnumerable<BlogPost> GetAllBlogPosts();
    BlogPost? GetBlogPostById(int? blogPostId);
    void AddBlogPost(BlogPost blogPost);
    void DeleteBlogPost(int blogPostId);
    void DeleteBlogPosts(List<BlogPost> blogPosts);
    void UpdateBlogPost(BlogPost blogPost);


    IEnumerable<Comment> GetAllComments();
    Comment? GetCommentById(int? commentId);
    void AddComment(Comment comment);
    void DeleteComment(int blogPostId);
    void DeleteComments(List<Comment> comments);
    void UpdateComment(Comment comment);


    void Save();
}

