namespace QuizGame.Api.Contracts.V1;

public record AnswerCreateRequest(Guid QuestionId, string Text, bool IsCorrect);
