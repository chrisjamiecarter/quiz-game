namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to update a quiz entity.
/// </summary>
public record QuizUpdateRequest(string Name, string Description, string ImageUrl);
