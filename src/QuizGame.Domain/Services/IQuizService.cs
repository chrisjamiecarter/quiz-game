using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="Quiz"/> entities.
/// </summary>
public interface IQuizService
{
    Task<bool> CreateAsync(Quiz quiz);
    Task<bool> DeleteAsync(Quiz quiz);
    Task<IEnumerable<Quiz>> ReturnAllAsync();
    Task<Quiz?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Quiz quiz);
}
