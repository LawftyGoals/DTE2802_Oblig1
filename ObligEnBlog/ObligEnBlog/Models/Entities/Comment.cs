namespace ObligEnBlog.Models.Entities;
public class Comment {
    public int CommentId { get; set; }
    public int BlogPostParentId { get; set; }
    public string CommentText { get; set; }


}
