using Asp.Versioning;
using Microsoft.Extensions.Options;
using QuizGame.Api.OpenApi;
using QuizGame.Api.Routes;
using QuizGame.Infrastructure.Installers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuizGame.Api.Installers;

/// <summary>
/// Registers dependencies and adds any required middleware for the Api layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

        return services;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        app.MapQuizGameEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
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
