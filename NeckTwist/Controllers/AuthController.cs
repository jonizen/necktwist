using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeckTwist.Models;

namespace NeckTwist.Controllers{
    
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AuthController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    // POST: /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel vm)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _signInManager.PasswordSignInAsync(
            userName: vm.Username,
            password: vm.Password,
            isPersistent: true,
            lockoutOnFailure: true
        );

        if (result.RequiresTwoFactor)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        if (result.IsLockedOut)
        {
            return StatusCode(StatusCodes.Status423Locked);
        }
        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized();
    }

    // POST: /api/auth/logout
    [Authorize, HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

}
}