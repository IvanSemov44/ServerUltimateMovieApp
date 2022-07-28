using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.MovieUser
{
    public class MovieUserForRegistrationDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is Reqired?")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Reqired?")]
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<string>? Roles { get; set; }
    }
}
