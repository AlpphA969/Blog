using System.Text.Json;
using System.Text.Json.Serialization;
using Blog.Endpiont.Api.Extensions;
using Blog.Endpiont.Api.Filters;
using Scalar.AspNetCore;
namespace Blog.Endpiont.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.AddAuroFac();
        builder.Services.AddIdentitySetup(builder.Configuration);
        builder.Services.AddDataBase(builder.Configuration);
        builder.Services.AddApiErrorHandling();
        
        
        builder.Services
            .AddControllers(options => { options.Filters.Add<ResultFilter>(); })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    JsonIgnoreCondition.WhenWritingNull;
            });
        
        
        
        
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();


        
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        app.UseStaticFiles();
        
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