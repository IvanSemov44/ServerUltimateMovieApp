using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class MovieUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LasttName { get; set; } = string.Empty;

        public string Favorite { get; set; } = string.Empty;

    }
}
