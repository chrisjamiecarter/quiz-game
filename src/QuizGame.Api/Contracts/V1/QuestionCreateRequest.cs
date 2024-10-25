namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to create a question entity.
/// </summary>
public record QuestionCreateRequest(Guid QuizId, string Text);
