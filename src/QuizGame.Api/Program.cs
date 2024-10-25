using QuizGame.Api.Installers;
using QuizGame.Application.Installers;
using QuizGame.Infrastructure.Installers;

namespace QuizGame.Api;

/// <summary>
/// The entry point for the API.
/// This class is responsible for configuring and launching the application.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApi()
                        .AddApplication()
                        .AddInfrastructure(builder.Configuration);

        var app = builder.Build();
        app.AddMiddleware()
           .SetUpDatabase()
           .Run();
    }
}
