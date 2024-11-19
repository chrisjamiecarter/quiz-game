namespace QuizGame.Domain.Entities;

/// <summary>
/// Represents a Game entity within the Domain layer.
/// </summary>
public class Game
{
    public required Guid Id { get; set; }

    public required DateTime Played { get; set; }

    public required int Score { get; set; }

    public required int MaxScore { get; set; }

    public required Guid QuizId { get; set; }

    public Quiz? Quiz { get; set; }
}
