using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ObligEnBlog.Models.Entities {
    public class Blog : IOwnerEntity {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool Active { get; set; } = true;
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }

    }
}
