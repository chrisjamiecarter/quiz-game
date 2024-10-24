namespace QuizGame.Infrastructure.Models;

/// <summary>
/// Represents a Question model in the Infrastructure layer.
/// </summary>
internal class QuestionModel
{
    public required Guid Id { get; set; }

    public required string Text { get; set; }

    public ICollection<AnswerModel> Answers { get; set; } = [];

    public required Guid QuizId { get; set; }

    public QuizModel? Quiz { get; set; }

}
