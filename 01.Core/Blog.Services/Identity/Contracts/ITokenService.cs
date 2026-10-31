using Blog.Entities.Identity;

namespace Services.Identity.Contracts;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
    
}