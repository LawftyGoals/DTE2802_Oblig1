using Microsoft.AspNetCore.Identity;

namespace ObligEnBlog.Models.Entities {
    public interface IOwnerEntity {
        IdentityUser Owner { get; set; }
    }
}
