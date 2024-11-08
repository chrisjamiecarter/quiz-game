namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning a game as an API response back to 
/// the client.
/// </summary>
public record GameResponse(Guid Id, Guid QuizId, string QuizName, DateTime Played, int Score);
