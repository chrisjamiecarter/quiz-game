namespace QuizGame.Domain.Entities;

/// <summary>
/// Represents an Answer entity within the Domain layer.
/// </summary>
public class Answer
{
    public required Guid Id { get; set; }

    public required string Text { get; set; }

    public required bool IsCorrect { get; set; }
}
