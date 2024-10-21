using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizGame.Infrastructure.Installers;

/// <summary>
/// Registers dependencies and seeds data for the Infrastructure layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        return serviceProvider;
    }
}
