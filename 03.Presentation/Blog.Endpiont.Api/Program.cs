using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Blog.Identity.Persistence.EF;
using Blog.PersistenceEF;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Endpiont.Api.Extensions;
using Blog.Endpiont.Api.Filters;
using Blog.Entities.Identity;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
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

        builder.Services.AddIdentitySetup(builder.Configuration);
        
        
        builder.Services
            .AddControllers(options => { options.Filters.Add<ResultFilter>(); })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    JsonIgnoreCondition.WhenWritingNull;
            });
        builder.Services.AddApiErrorHandling();
        
        
        
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();


        
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();



        app.MapControllers();
        
        app.Run();
    }
}