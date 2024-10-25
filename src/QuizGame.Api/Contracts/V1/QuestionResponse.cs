namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning a question as an API response back to 
/// the client.
/// </summary>
public record QuestionResponse(Guid Id, Guid QuizId, string Text);
