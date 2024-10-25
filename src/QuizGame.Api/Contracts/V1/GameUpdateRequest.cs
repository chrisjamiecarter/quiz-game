namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to update a game entity.
/// </summary>
public record GameUpdateRequest(DateTime Played, int Score);
