using Blog.Identity.Persistence.EF;
using Blog.PersistenceEF;
using Microsoft.EntityFrameworkCore;

namespace Blog.Endpiont.Api.Extensions;

public static class DataBaseSetupExtension
{
    public static IServiceCollection AddDataBase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EFDataContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        
        services.AddDbContext<EFIdentityDataContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        
        
        
        return services;
    }
    
}