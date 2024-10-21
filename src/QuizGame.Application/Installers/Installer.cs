using Microsoft.Extensions.DependencyInjection;

namespace QuizGame.Application.Installers;

/// <summary>
/// Registers dependencies for the Application layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
