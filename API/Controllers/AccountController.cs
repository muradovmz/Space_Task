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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await Mediator.Send(new Login.Command {Login=loginDto});
            return HandleResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await Mediator.Send(new Register.Command { Registration = registerDto });
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateDto updateDto)
        {
            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var result = await Mediator.Send(new Update.Command { Update = updateDto , User = currentUser});
            return HandleResult(result);
        }

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