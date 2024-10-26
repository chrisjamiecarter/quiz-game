using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Application.Repositories;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Repositories;
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

        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ISeederService, SeederService>();

        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider, string environmentName)
    {
        var context = serviceProvider.GetRequiredService<QuizGameDataContext>();

        switch (environmentName)
        {
            case "IntegrationTesting":
                context.Database.EnsureCreated();
                break;
            default:
                context.Database.Migrate();
                break;
        }

        var seeder = serviceProvider.GetRequiredService<ISeederService>();
        seeder.SeedDatabase();

        return serviceProvider;
    }
}
