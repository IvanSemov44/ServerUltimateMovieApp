using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MovieUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Favorite { get; set; } = string.Empty;

        public ICollection<Movie>? Movies { get; set; } = new List<Movie>();

    }
}
