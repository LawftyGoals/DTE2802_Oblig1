namespace ObligEnBlog.Models.Entities {
    public class BlogPost {
        public int BlogPostId { get; set; }
        public int BlogParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

    }
}
