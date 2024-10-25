using QuizGame.Domain.Entities;

namespace QuizGame.Application.Repositories;

/// <summary>
/// Defines the contract for performing CRUD operations on <see cref="Quiz"/> entities in the
/// data store.
/// </summary>
public interface IQuizRepository
{
    Task CreateAsync(Quiz quiz);
    Task DeleteAsync(Quiz quiz);
    Task<IReadOnlyList<Quiz>> ReturnAsync();
    Task<Quiz?> ReturnAsync(Guid id);
    Task UpdateAsync(Quiz quiz);
}
