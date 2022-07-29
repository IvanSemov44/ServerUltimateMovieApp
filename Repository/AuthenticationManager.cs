using Constracts;
using Entities.DataTransferObjects.MovieUser;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Repository
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<MovieUser> _userManager;

        private MovieUser _movieUser = new MovieUser();

        public AuthenticationManager(UserManager<MovieUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }


        public async Task<bool> ValidateUser(MovieUserForAuthenticationDto userForAuthenticationDto)
        {
            _movieUser = await _userManager.FindByNameAsync(userForAuthenticationDto.Username);

            return (_movieUser != null && await _userManager.CheckPasswordAsync(_movieUser, userForAuthenticationDto.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return  new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:secret"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

         private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _movieUser.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_movieUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(

                issuer: _configuration["JwtSettings:validIssuer"],
                audience: _configuration["JwtSettings:validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
