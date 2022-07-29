using Entities.DataTransferObjects.MovieUser;

namespace Constracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(MovieUserForAuthenticationDto movieUserForAuthenticationDto);

        Task<string> CreateToken();
    }
}
