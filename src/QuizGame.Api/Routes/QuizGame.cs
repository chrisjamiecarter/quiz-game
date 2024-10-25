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
        app.MapAnswerEndpoints();

        return app;
    }

    private static WebApplication MapAnswerEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("/api/v1/quizgame/answers")
                         .WithOpenApi();

        builder.MapGet("/", AnswerEndpoints.GetAnswers)
               .WithName(nameof(AnswerEndpoints.GetAnswers))
               .WithSummary("Get all Quiz Game answers.");

        builder.MapGet("/{id}", AnswerEndpoints.GetAnswer)
               .WithName(nameof(AnswerEndpoints.GetAnswer))
               .WithSummary("Get a Quiz Game answer by ID.");

        builder.MapGet("/quiz/{id}", AnswerEndpoints.GetQuestionAnswers)
               .WithName(nameof(AnswerEndpoints.GetQuestionAnswers))
               .WithSummary("Gets all Quiz Game answers for a question ID.");

        builder.MapPost("/", AnswerEndpoints.CreateAnswer)
               .WithName(nameof(AnswerEndpoints.CreateAnswer))
               .WithSummary("Create a new Quiz Game answer.");

        builder.MapPut("/{id}", AnswerEndpoints.UpdateAnswer)
               .WithName(nameof(AnswerEndpoints.UpdateAnswer))
               .WithSummary("Update an existing Quiz Game answer.");

        builder.MapDelete("/{id}", AnswerEndpoints.DeleteAnswer)
               .WithName(nameof(AnswerEndpoints.DeleteAnswer))
               .WithSummary("Delete an existing Quiz Game answer.");

        return app;
    }
}
