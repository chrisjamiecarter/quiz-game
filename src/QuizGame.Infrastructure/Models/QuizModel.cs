namespace QuizGame.Infrastructure.Models;

/// <summary>
/// Represents a Quiz model in the Infrastructure layer.
/// </summary>
internal class QuizModel
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = "";

    public required ICollection<GameModel> Games { get; set; } = [];

    public required ICollection<QuestionModel> Questions { get; set; } = [];
}
