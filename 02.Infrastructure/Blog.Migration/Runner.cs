using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Migration;

class Runner
{
    static void Main(string[] args)
    {
        var connectionString =
            "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=P@ssw0rd23";

        using var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres15_0()
                .WithGlobalCommandTimeout(TimeSpan.FromSeconds(300))
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Runner).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
    }
}