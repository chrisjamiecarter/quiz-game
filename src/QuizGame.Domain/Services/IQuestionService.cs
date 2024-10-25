using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="Question"/> entities.
/// </summary>
public interface IQuestionService
{
    Task<bool> CreateAsync(Question question);
    Task<bool> DeleteAsync(Question question);
    Task<IEnumerable<Question>> ReturnAllAsync();
    Task<Question?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Question question);
}
