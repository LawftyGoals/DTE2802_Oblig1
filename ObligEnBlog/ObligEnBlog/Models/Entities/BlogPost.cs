namespace ObligEnBlog.Models.Entities
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public Blog BlogParent {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

    }
}
