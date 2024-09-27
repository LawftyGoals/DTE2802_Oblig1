using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models.ViewModels {
    public class BlogPostDetailsViewModel {
        public IEnumerable<BlogPost>? BlogPosts { get; set; }
        public BlogPost? BlogPost { get; set; }
        public Blog? Blog { get; set; }

        public IEnumerable<Comment>? Comments { get; set; }

        public Comment? Comment { get; set; }


    }
}
