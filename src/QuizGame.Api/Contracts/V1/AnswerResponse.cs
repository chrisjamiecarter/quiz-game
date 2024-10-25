namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning an answer as an API response back to 
/// the client.
/// </summary>
public record AnswerResponse(Guid Id, Guid QuestionId, string Text, bool IsCorrect);
