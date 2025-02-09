using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using run_script.Models;
using run_script.Repositories;

namespace run_script.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        // post /api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, request.Password);
            // check if its succeed
            if (identityResult.Succeeded)
            {
                // add role to this user
                if (request.Roles != null && request.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, request.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Now Login!");
                    }
                }
            }


            return BadRequest("Something went wrong!");

        }

        // POST : /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // create token 

                    // take the role of the user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null && roles.Any())
                    {
                        // create token 
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            token = token,
                        };
                        return Ok(response);
                    }
                }
                return BadRequest("Password Incorect!");
            }
            return BadRequest("Email or Password Incorect!");
        }
    }
}
