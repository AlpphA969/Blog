using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Identity.Contracts;
using Services.Identity.Contracts.Dto;

namespace Blog.Endpiont.Api;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    
    private readonly IUserService  _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IdentityResult> Register([FromBody] UserDto user)
    {
        var result = await _userService.Register(user);
        return result;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<Result<string>> Login([FromBody] UserDto user)
    {
        var result = await _userService.Login(user);
        return result;
    }

    [HttpGet("me")]
    public IActionResult Me() =>
        Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    
    
    
}