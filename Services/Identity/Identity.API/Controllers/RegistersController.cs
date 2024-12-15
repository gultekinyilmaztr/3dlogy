using Identity.API.Dtos;
using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class RegistersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public RegistersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
    {
        var values = new ApplicationUser()
        {
            UserName = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            Name = userRegisterDto.Name,
            Surname = userRegisterDto.Surname
        };
        var result = await _userManager.CreateAsync(values, userRegisterDto.Password);
        if (result.Succeeded)
        {
            return StatusCode(StatusCodes.Status201Created);
        }

        return BadRequest(result.Errors);
    }
}

