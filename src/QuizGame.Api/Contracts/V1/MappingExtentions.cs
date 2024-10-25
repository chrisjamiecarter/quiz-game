using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class MappingExtensions
{
    public static Answer ToDomain(this AnswerCreateRequest request)
    {
        return new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = request.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect,
        };
    }

    public static AnswerResponse ToResponse(this Answer entity)
    {
        return new AnswerResponse(entity.Id, entity.QuestionId, entity.Text, entity.IsCorrect);
    }

    public static Game ToDomain(this GameCreateRequest request)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Played = request.Played,
            Score = request.Score,
        };
    }

    public static GameResponse ToResponse(this Game entity)
    {
        return new GameResponse(entity.Id, entity.QuizId, entity.Played, entity.Score);
    }

    public static Question ToDomain(this QuestionCreateRequest request)
    {
        return new Question
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Text = request.Text,
        };
    }

    public static QuestionResponse ToResponse(this Question entity)
    {
        return new QuestionResponse(entity.Id, entity.QuizId, entity.Text);
    }
}
