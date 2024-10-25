namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to create an answer entity.
/// </summary>
public record AnswerCreateRequest(Guid QuestionId, string Text, bool IsCorrect);
