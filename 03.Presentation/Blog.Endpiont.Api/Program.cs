using Blog.Identity.Persistence.EF;
using Blog.PersistenceEF;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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


        
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}