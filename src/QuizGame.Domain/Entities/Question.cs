namespace QuizGame.Domain.Entities;

/// <summary>
/// Represents a Question entity within the Domain layer.
/// </summary>
public class Question
{
    public required Guid Id { get; set; }

    public required string Text { get; set; }

    public required Guid QuizId { get; set; }

    public Quiz? Quiz { get; set; }

    public ICollection<Answer> Answers { get; set; } = [];
}
