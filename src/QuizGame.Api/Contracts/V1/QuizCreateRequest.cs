namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to create a quiz entity.
/// </summary>
public record QuizCreateRequest(string Name, string Description, string ImageUrl);
