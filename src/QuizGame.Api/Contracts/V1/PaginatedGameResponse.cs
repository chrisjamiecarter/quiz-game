namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning a paginated game API response.
/// </summary>
public record PaginatedGameResponse(int TotalRecords, IReadOnlyList<GameResponse> Games);
