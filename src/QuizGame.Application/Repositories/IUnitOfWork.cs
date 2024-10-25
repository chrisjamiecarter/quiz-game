namespace QuizGame.Application.Repositories;

/// <summary>
/// Represents a unit of work pattern interface for coordinating changes across multiple repositories in the Application.
/// </summary>
public interface IUnitOfWork
{
    IAnswerRepository Answers { get; set; }
    IGameRepository Games{ get; set; }
    IQuestionRepository Questions { get; set; }
    IQuizRepository Quizzes { get; set; }
    Task<int> SaveAsync();
}
