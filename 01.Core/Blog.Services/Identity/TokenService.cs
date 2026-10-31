using System.Security.Claims;
using System.Text;
using Blog.Entities.Identity;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Identity.Contracts;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Services.Identity;

public class TokenService : ITokenService
{
    private readonly JwtSetting _jwtSettings;
    private readonly UserManager<User> _userManager;

    public TokenService(JwtSetting jwtSetting, UserManager<User> userManager)
    {
        _jwtSettings = jwtSetting;
        _userManager = userManager;
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var roles =  await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );
        
        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        
        return serializedToken;

        
    }
}