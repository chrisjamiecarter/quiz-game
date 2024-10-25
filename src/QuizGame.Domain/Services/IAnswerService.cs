using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="Answer"/> entities.
/// </summary>
public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer);
    Task<bool> DeleteAsync(Answer answer);
    Task<IEnumerable<Answer>> ReturnAll();
    Task<Answer?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Answer answer);
}
