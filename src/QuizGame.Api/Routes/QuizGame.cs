using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using QuizGame.Api.Contracts.V1;
using QuizGame.Api.Endpoints;
using QuizGame.Api.Filters;

namespace QuizGame.Api.Routes;

/// <summary>
/// Defines the mapped API routes for the applications endpoints. 
/// Supports versioned APIs using Asp.Versioning.
/// </summary>
public static class QuizGame
{
    public static WebApplication MapQuizGameEndpoints(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
                               .HasApiVersion(1)
                               .ReportApiVersions()
                               .Build();

        app.MapAnswerEndpoints(apiVersionSet)
           .MapGameEndpoints(apiVersionSet)
           .MapQuestionEndpoints(apiVersionSet)
           .MapQuizEndpoints(apiVersionSet);

        return app;
    }

    private static WebApplication MapAnswerEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var builder = app.MapGroup("/api/v{version:apiVersion}/quizgame/answers")
                         .WithApiVersionSet(apiVersionSet)
                         .WithOpenApi();

        builder.MapGet("/", AnswerEndpoints.GetAnswersAsync)
               .WithName(nameof(AnswerEndpoints.GetAnswersAsync))
               .WithSummary("Get all Quiz Game answers.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}", AnswerEndpoints.GetAnswerAsync)
               .WithName(nameof(AnswerEndpoints.GetAnswerAsync))
               .WithSummary("Get a Quiz Game answer by ID.")
               .MapToApiVersion(1);

        builder.MapPost("/", AnswerEndpoints.CreateAnswerAsync)
               .WithName(nameof(AnswerEndpoints.CreateAnswerAsync))
               .WithSummary("Create a new Quiz Game answer.")
               .MapToApiVersion(1)
               .WithRequestValidation<AnswerCreateRequest>();

        builder.MapPut("/{id}", AnswerEndpoints.UpdateAnswerAsync)
               .WithName(nameof(AnswerEndpoints.UpdateAnswerAsync))
               .WithSummary("Update an existing Quiz Game answer.")
               .MapToApiVersion(1)
               .WithRequestValidation<AnswerUpdateRequest>();

        builder.MapDelete("/{id}", AnswerEndpoints.DeleteAnswerAsync)
               .WithName(nameof(AnswerEndpoints.DeleteAnswerAsync))
               .WithSummary("Delete an existing Quiz Game answer.")
               .MapToApiVersion(1);

        return app;
    }

    private static WebApplication MapGameEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var builder = app.MapGroup("/api/v{version:apiVersion}/quizgame/games")
                         .WithApiVersionSet(apiVersionSet)
                         .WithOpenApi();
        
        builder.MapGet("/", GameEndpoints.GetGamesAsync)
               .WithName(nameof(GameEndpoints.GetGamesAsync))
               .WithSummary("Get all Quiz Game games.")
               .MapToApiVersion(1);

        builder.MapGet("/page", GameEndpoints.GetPaginatedGamesAsync)
               .WithName(nameof(GameEndpoints.GetPaginatedGamesAsync))
               .WithSummary("Get a paginated list of Quiz Game games.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}", GameEndpoints.GetGameAsync)
               .WithName(nameof(GameEndpoints.GetGameAsync))
               .WithSummary("Get a Quiz Game game by ID.")
               .MapToApiVersion(1);

        builder.MapPost("/", GameEndpoints.CreateGameAsync)
               .WithName(nameof(GameEndpoints.CreateGameAsync))
               .WithSummary("Create a new Quiz Game game.")
               .MapToApiVersion(1)
               .WithRequestValidation<GameCreateRequest>();

        return app;
    }

    private static WebApplication MapQuestionEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var builder = app.MapGroup("/api/v{version:apiVersion}/quizgame/questions")
                         .WithApiVersionSet(apiVersionSet)
                         .WithOpenApi();

        builder.MapGet("/", QuestionEndpoints.GetQuestionsAsync)
               .WithName(nameof(QuestionEndpoints.GetQuestionsAsync))
               .WithSummary("Get all Quiz Game questions.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}", QuestionEndpoints.GetQuestionAsync)
               .WithName(nameof(QuestionEndpoints.GetQuestionAsync))
               .WithSummary("Get a Quiz Game question by ID.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}/answers", QuestionEndpoints.GetQuestionAnswersAsync)
               .WithName(nameof(QuestionEndpoints.GetQuestionAnswersAsync))
               .WithSummary("Get all answers for a Quiz Game question by ID.")
               .MapToApiVersion(1);

        builder.MapPost("/", QuestionEndpoints.CreateQuestionAsync)
               .WithName(nameof(QuestionEndpoints.CreateQuestionAsync))
               .WithSummary("Create a new Quiz Game question.")
               .MapToApiVersion(1)
               .WithRequestValidation<QuestionCreateRequest>();

        builder.MapPut("/{id}", QuestionEndpoints.UpdateQuestionAsync)
               .WithName(nameof(QuestionEndpoints.UpdateQuestionAsync))
               .WithSummary("Update an existing Quiz Game question.")
               .MapToApiVersion(1)
               .WithRequestValidation<QuestionUpdateRequest>();

        builder.MapDelete("/{id}", QuestionEndpoints.DeleteQuestionAsync)
               .WithName(nameof(QuestionEndpoints.DeleteQuestionAsync))
               .WithSummary("Delete an existing Quiz Game question.")
               .MapToApiVersion(1);

        return app;
    }

    private static WebApplication MapQuizEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var builder = app.MapGroup("/api/v{version:apiVersion}/quizgame/quizzes")
                         .WithApiVersionSet(apiVersionSet)
                         .WithOpenApi();

        builder.MapGet("/", QuizEndpoints.GetQuizzesAsync)
               .WithName(nameof(QuizEndpoints.GetQuizzesAsync))
               .WithSummary("Get all Quiz Game quizzes.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}", QuizEndpoints.GetQuizAsync)
               .WithName(nameof(QuizEndpoints.GetQuizAsync))
               .WithSummary("Get a Quiz Game quiz by ID.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}/games", QuizEndpoints.GetQuizGamesAsync)
               .WithName(nameof(QuizEndpoints.GetQuizGamesAsync))
               .WithSummary("Get all games for a Quiz Game quiz by ID.")
               .MapToApiVersion(1);

        builder.MapGet("/{id}/questions", QuizEndpoints.GetQuizQuestionsAsync)
               .WithName(nameof(QuizEndpoints.GetQuizQuestionsAsync))
               .WithSummary("Get all questions for a Quiz Game quiz by ID.")
               .MapToApiVersion(1);
        
        builder.MapPost("/", QuizEndpoints.CreateQuizAsync)
               .WithName(nameof(QuizEndpoints.CreateQuizAsync))
               .WithSummary("Create a new Quiz Game quiz.")
               .MapToApiVersion(1)
               .WithRequestValidation<QuizCreateRequest>();

        builder.MapPut("/{id}", QuizEndpoints.UpdateQuizAsync)
               .WithName(nameof(QuizEndpoints.UpdateQuizAsync))
               .WithSummary("Update an existing Quiz Game quiz.")
               .MapToApiVersion(1)
               .WithRequestValidation<QuizUpdateRequest>();

        builder.MapDelete("/{id}", QuizEndpoints.DeleteQuizAsync)
               .WithName(nameof(QuizEndpoints.DeleteQuizAsync))
               .WithSummary("Delete an existing Quiz Game quiz.")
               .MapToApiVersion(1);

        builder.MapDelete("/{id}/questions", QuizEndpoints.DeleteQuizQuestionsAsync)
               .WithName(nameof(QuizEndpoints.DeleteQuizQuestionsAsync))
               .WithSummary("Delete all questions for a Quiz Game quiz by ID.")
               .MapToApiVersion(1);

        return app;
    }
}
