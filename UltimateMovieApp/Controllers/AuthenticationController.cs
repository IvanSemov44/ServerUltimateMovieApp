using AutoMapper;
using Entities.DataTransferObjects.MovieUser;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;

namespace UltimateMovieApp.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<MovieUser> _userManager;

        public AuthenticationController(IMapper mapper, UserManager<MovieUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser(
            [FromBody] MovieUserForRegistrationDto movieUserForRegistration)
        {

            var user = _mapper.Map<MovieUser>(movieUserForRegistration);

            var result = await _userManager.CreateAsync(user, movieUserForRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, movieUserForRegistration.Roles);

            return StatusCode(201);
        }
    }
}
