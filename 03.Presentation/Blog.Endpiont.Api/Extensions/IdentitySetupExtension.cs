using System.Text;
using Blog.Entities.Identity;
using Blog.Identity.Persistence.EF;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Endpiont.Api.Extensions;

public static class IdentitySetupExtension
{

    public static IServiceCollection AddIdentitySetup(
        this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings =  configuration.GetSection(JwtSetting.SectionName).Get<JwtSetting>();
        services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SectionName));
        services.AddSingleton(jwtSettings);
        
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        
        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<EFIdentityDataContext>()
            .AddDefaultTokenProviders();
        return services;

    }
    
}