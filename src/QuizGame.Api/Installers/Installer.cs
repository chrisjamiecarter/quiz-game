using QuizGame.Api.Routes;
using QuizGame.Infrastructure.Installers;

namespace QuizGame.Api.Installers;

/// <summary>
/// Registers dependencies and adds any required middleware for the Api layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        app.MapQuizGameEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(options =>
        {
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

        return app;
    }

    public static WebApplication SetUpDatabase(this WebApplication app)
    {
        // Performs any database migrations and seeds the database.
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        services.SeedDatabase(app.Environment.EnvironmentName);

        return app;
    }
}
