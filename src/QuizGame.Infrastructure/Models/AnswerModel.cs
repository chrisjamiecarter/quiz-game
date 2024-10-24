namespace QuizGame.Infrastructure.Models;

/// <summary>
/// Represents an Answer model in the Infrastructure layer.
/// </summary>
internal class AnswerModel
{
    public required Guid Id { get; set; }

    public required string Text { get; set; }

    public required bool IsCorrect { get; set; }

    public required Guid QuestionId { get; set; }

    public QuestionModel? Question { get; set; }
}
