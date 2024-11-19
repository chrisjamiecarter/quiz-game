namespace QuizGame.Infrastructure.Models;

/// <summary>
/// Represents a Game model in the Infrastructure layer.
/// </summary>
internal class GameModel
{
    public required Guid Id { get; set; }

    public required DateTime Played { get; set; }

    public required int Score { get; set; }

    public required int MaxScore { get; set; }

    public required Guid QuizId { get; set; }

    public QuizModel? Quiz { get; set; }
}
