using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Application.Options;
using QuizGame.Application.Repositories;
using QuizGame.Application.Services;
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

        var userSecrets = new Dictionary<string, string?>
        {
            { "<database-server>", configuration["<database-server>"] },
            { "<database-user>", configuration["<database-user>"] },
            { "<database-user-password>", configuration["<database-user-password>"] }
        };
        connectionString = connectionString.ReplaceUserSecrets(userSecrets);

        services.AddDbContext<QuizGameDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddMemoryCache();
        services.AddScoped<ICacheService, DatabaseCacheService>();
        services.AddOptions<CacheOptions>().Bind(configuration.GetSection(nameof(CacheOptions)));

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

    private static string ReplaceUserSecrets(this string source, IDictionary<string, string?> secrets)
    {
        foreach (var (key, value) in secrets)
        {
            source = value != null ? source.Replace(key, value) : source;
        }

        return source;
    }
}
