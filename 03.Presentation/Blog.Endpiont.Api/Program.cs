using System.Text;
using Blog.Identity.Persistence.EF;
using Blog.PersistenceEF;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Entities.Identity;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services;
namespace Blog.Endpiont.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(container =>
        {
            container.RegisterModule<PersistenceEF.DependencyInjection>();
            container.RegisterModule<Infrastructure.DependencyInjection>();
            container.RegisterModule<Blog.Identity.Persistence.EF.DependencyInjection>();
            container.RegisterModule<Services.DependencyInjection>();
            
        });
        
        builder.Services.AddDbContext<EFDataContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddDbContext<EFIdentityDataContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        
        
        var jwtSettings = builder.Configuration.GetSection(JwtSetting.SectionName).Get<JwtSetting>()
                          ?? throw new InvalidOperationException("JWT settings are not configured.");
        builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.SectionName));
        builder.Services.AddSingleton(jwtSettings);
        
        
        builder.Services
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
        
        
        builder.Services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<EFIdentityDataContext>()
            .AddDefaultTokenProviders();
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();


        
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization();
        
        app.Run();
    }
}