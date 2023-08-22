using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Repositories;
using NZWalksAPI.Services;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenService) 
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestData data)
        {
            var identityUser = new IdentityUser
            {
                UserName = data.Username,
                Email = data.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, data.Password);
            
            if (!identityResult.Succeeded) 
            {
                return BadRequest("Failed to register! Something went wrong!");
            }

            if (data.Roles == null || !data.Roles.Any())
            {
                return BadRequest("No roles were specified!");
            }

            identityResult = await userManager.AddToRolesAsync(identityUser, data.Roles);

            if (!identityResult.Succeeded)
            {
                return BadRequest("Failed to add roles! Something went wrong!");
                
            }

            return Ok("User was register! Please login :)");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestData data)
        {
            var user = await userManager.FindByEmailAsync(data.Username);

            if (user == null) 
            {
                return BadRequest("Username cannot be found");
            }

            var login = await userManager.CheckPasswordAsync(user, data.Password);

            if (login)
            {
                var roles = await userManager.GetRolesAsync(user);

                if(roles == null)
                {
                    return BadRequest("User dont have any roles");
                }

                var jwtToken = tokenService.CreateJWTToken(user, roles.ToList());

                var response = new LoginResponseData
                {
                    JwtToken = jwtToken!
                };

                return Ok(response);
            }

            return BadRequest("Password was incorrect!");
        }
    }
}
