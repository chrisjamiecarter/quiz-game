using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Services;

namespace QuizGame.Infrastructure.Installers;

/// <summary>
/// Registers dependencies and seeds data for the Infrastructure layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("QuizGame") ?? throw new InvalidOperationException("Connection string 'QuizGame' not found");

        services.AddDbContext<QuizGameDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ISeederService, SeederService>();

        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<QuizGameDataContext>();
        context.Database.Migrate();

        var seeder = serviceProvider.GetRequiredService<ISeederService>();
        seeder.SeedDatabase();

        return serviceProvider;
    }
}
