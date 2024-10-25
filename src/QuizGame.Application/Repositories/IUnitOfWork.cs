namespace QuizGame.Application.Repositories;

/// <summary>
/// Represents a unit of work pattern interface for coordinating changes across multiple repositories in the Application.
/// </summary>
public interface IUnitOfWork
{
    IAnswerRepository Answers { get; }
    IGameRepository Games{ get; }
    IQuestionRepository Questions { get; }
    IQuizRepository Quizzes { get; }
    Task<int> SaveAsync();
}
