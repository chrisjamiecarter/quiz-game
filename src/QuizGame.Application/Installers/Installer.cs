using Microsoft.Extensions.DependencyInjection;
using QuizGame.Application.Services;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Installers;

/// <summary>
/// Registers dependencies for the Application layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnswerService, AnswerService>();

        return services;
    }
}
