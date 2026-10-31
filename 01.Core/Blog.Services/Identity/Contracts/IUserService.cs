using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Services.Identity.Contracts.Dto;

namespace Services.Identity.Contracts;

public interface IUserService
{
    Task<IdentityResult> Register(UserDto user);
    Task<Result<string>> Login(UserDto user);

}