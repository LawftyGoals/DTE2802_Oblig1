using System.ComponentModel.DataAnnotations;

namespace ObligEnBlog.Models.Entities;
public class Comment {
    public int CommentId { get; set; }
    public int BlogPostParentId { get; set; }
    [Display(Name = "Comment")]
    public string CommentText { get; set; }

    [Display(Name = "Date Created")]
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; } = DateTime.Now;




}
