namespace QuizGame.Api.Contracts.V1;

public record AnswerResponse(Guid Id, Guid QuestionId, string Text, bool IsCorrect);
