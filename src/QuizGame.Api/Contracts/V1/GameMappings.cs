using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class GameMappings
{
    public static Game ToDomain(this GameCreateRequest request)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Played = request.Played,
            Score = request.Score,
            MaxScore = request.MaxScore,
        };
    }

    public static GameResponse ToResponse(this Game entity)
    {
        return new GameResponse(entity.Id, entity.QuizId, entity.Quiz?.Name ?? "", entity.Played, entity.Score, entity.MaxScore);
    }
}
