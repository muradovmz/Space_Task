using System.Security.Claims;
using System.Threading.Tasks;
using Application.Users;
using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Authentification and Autorization
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>JWT Token</returns>
        /// <response code="200">Returns the newly created token</response>
        /// <response code="401">If the login is failed</response>  
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await Mediator.Send(new Login.Command {Login=loginDto});
            return HandleResult(result);
        }

        /// <summary>
        /// Registration for new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <response code="200">Returns Success status</response>
        /// <response code="400">If the registation is failed</response> 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await Mediator.Send(new Register.Command { Registration = registerDto });
            return HandleResult(result);
        }


        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="updateDto"></param>
        /// <response code="200">Returns Success status</response>
        /// <response code="400">If the patching is failed</response> 
        [Authorize]
        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateDto updateDto)
        {
            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var result = await Mediator.Send(new Update.Command { Update = updateDto , User = currentUser});
            return HandleResult(result);
        }


        /// <summary>
        /// Delete user
        /// </summary>
        /// <response code="200">Returns Success status</response>
        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete()
        {
            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            await _userManager.DeleteAsync(currentUser);
            return Ok();
        }
    }
}