using QuizGame.Api.Endpoints;

namespace QuizGame.Api.Routes;

/// <summary>
/// Defines the mapped API routes for the applications endpoints. 
/// Supports versioned APIs using Asp.Versioning.
/// </summary>
public static class QuizGame
{
    public static WebApplication MapQuizGameEndpoints(this WebApplication app)
    {
        app.MapAnswerEndpoints()
           .MapGameEndpoints();

        return app;
    }

    private static WebApplication MapAnswerEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("/api/v1/quizgame/answers")
                         .WithOpenApi();

        builder.MapGet("/", AnswerEndpoints.GetAnswersAsync)
               .WithName(nameof(AnswerEndpoints.GetAnswersAsync))
               .WithSummary("Get all Quiz Game answers.");

        builder.MapGet("/{id}", AnswerEndpoints.GetAnswerAsync)
               .WithName(nameof(AnswerEndpoints.GetAnswerAsync))
               .WithSummary("Get a Quiz Game answer by ID.");

        builder.MapGet("/quiz/{id}", AnswerEndpoints.GetQuestionAnswersAsync)
               .WithName(nameof(AnswerEndpoints.GetQuestionAnswersAsync))
               .WithSummary("Gets all Quiz Game answers for a question ID.");

        builder.MapPost("/", AnswerEndpoints.CreateAnswerAsync)
               .WithName(nameof(AnswerEndpoints.CreateAnswerAsync))
               .WithSummary("Create a new Quiz Game answer.");

        builder.MapPut("/{id}", AnswerEndpoints.UpdateAnswerAsync)
               .WithName(nameof(AnswerEndpoints.UpdateAnswerAsync))
               .WithSummary("Update an existing Quiz Game answer.");

        builder.MapDelete("/{id}", AnswerEndpoints.DeleteAnswerAsync)
               .WithName(nameof(AnswerEndpoints.DeleteAnswerAsync))
               .WithSummary("Delete an existing Quiz Game answer.");

        return app;
    }

    private static WebApplication MapGameEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("/api/v1/quizgame/games")
                         .WithOpenApi();

        builder.MapGet("/", GameEndpoints.GetGamesAsync)
               .WithName(nameof(GameEndpoints.GetGamesAsync))
               .WithSummary("Get all Quiz Game games.");

        builder.MapGet("/{id}", GameEndpoints.GetGameAsync)
               .WithName(nameof(GameEndpoints.GetGameAsync))
               .WithSummary("Get a Quiz Game game by ID.");

        builder.MapGet("/quiz/{id}", GameEndpoints.GetQuizGamesAsync)
               .WithName(nameof(GameEndpoints.GetQuizGamesAsync))
               .WithSummary("Gets all Quiz Game games for a quiz ID.");

        builder.MapPost("/", GameEndpoints.CreateGameAsync)
               .WithName(nameof(GameEndpoints.CreateGameAsync))
               .WithSummary("Create a new Quiz Game game.");

        builder.MapPut("/{id}", GameEndpoints.UpdateGameAsync)
               .WithName(nameof(GameEndpoints.UpdateGameAsync))
               .WithSummary("Update an existing Quiz Game game.");

        builder.MapDelete("/{id}", GameEndpoints.DeleteGameAsync)
               .WithName(nameof(GameEndpoints.DeleteGameAsync))
               .WithSummary("Delete an existing Quiz Game game.");

        return app;
    }
}
