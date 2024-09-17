using System.ComponentModel.DataAnnotations;

namespace ObligEnBlog.Models.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public IEnumerable<BlogPost> Posts { get; set; }

    }
}
