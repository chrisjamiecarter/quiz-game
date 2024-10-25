namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to create a game entity.
/// </summary>
public record GameCreateRequest(Guid QuizId, DateTime Played, int Score);
