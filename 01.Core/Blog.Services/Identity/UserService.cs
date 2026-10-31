using Blog.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Services.Identity.Contracts;
using Services.Identity.Contracts.Dto;
using Services.Identity.Contracts.Exception;

namespace Services.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<IdentityResult> Register(UserDto dto)
    {
        var userExist = await _userManager.FindByNameAsync(dto.Username);
        if (userExist != null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateUserName",
                Description = $"Username '{dto.Username}' is already taken."
            });
        }

        var User = new User
        {
            UserName = dto.Username,
        };
        var result = await _userManager.CreateAsync(User, dto.Password);
        return result;
        
    }

    public async Task<Result<string>> Login(UserDto dto)
    {
        
        var user = await _userManager.FindByNameAsync(dto.Username);
        
        if (user is null)
        {
            return UserExceptions.UserNotFound();

        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
        {
            return UserExceptions.WrongPassword();

        }

        var token = await _tokenService.GenerateTokenAsync(user);
        return token;
        
    }
}