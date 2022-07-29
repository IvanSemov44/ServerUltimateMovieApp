using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.MovieUser
{
    public class MovieUserForAuthenticationDto
    {
        [Required(ErrorMessage ="Username is Reqired?")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is Reqired?")]
        public string Password { get; set; } = string.Empty;
    }
}
