using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public Blog Blog { get; set; }


    }
}
