namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to update an answer entity.
/// </summary>
public record AnswerUpdateRequest(string Text, bool IsCorrect);
