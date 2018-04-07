using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeckTwist.Models;


namespace NeckTwist.Controllers{

    [Route("dev/seed")]
public class SeedController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        Role[] roles = (Role[])Enum.GetValues(typeof(Role));
        foreach(var r in roles)
        {
            var identityRole = new IdentityRole {
                Id = r.GetRoleName(),
                Name = r.GetRoleName()
            };

            if(!await _roleManager.RoleExistsAsync(roleName: identityRole.Name))
            {
                var result = await _roleManager.CreateAsync(identityRole);

                if(!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        ApplicationUser user = new ApplicationUser {
            FullName = "Jane Doe",
            Email = "janedoe@example.com",
            UserName = "janedoe@example.com",
            LockoutEnabled = false
        };

        if(await _userManager.FindByEmailAsync(user.Email) == null)
        {
            var result = await _userManager.CreateAsync(user, password: "5ESTdYB5cyYwA2dKhJqyjPYnKUc&45Ydw^gz^jy&FCV3gxpmDPdaDmxpMkhpp&9TRadU%wQ2TUge!TsYXsh77Qmauan3PEG8!6EP");

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);

            result = await _userManager.AddToRolesAsync(user, roles.Select(r => r.GetRoleName()));

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }
}
}