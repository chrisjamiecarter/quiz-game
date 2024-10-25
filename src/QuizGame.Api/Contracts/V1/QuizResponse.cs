namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning a quiz as an API response back to 
/// the client.
/// </summary>
public record QuizResponse(Guid Id, string Name, string Description);
