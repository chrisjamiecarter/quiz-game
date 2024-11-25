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
        
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
;       builder.Services.AddApi();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();
        app.AddMiddleware();
        app.SetUpDatabase();
        app.Run();
    }
}
