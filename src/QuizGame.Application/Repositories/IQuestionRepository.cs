using QuizGame.Domain.Entities;

namespace QuizGame.Application.Repositories;

/// <summary>
/// Defines the contract for performing CRUD operations on <see cref="Question"/> entities in the
/// data store.
/// </summary>
public interface IQuestionRepository
{
    Task CreateAsync(Question question);
    Task DeleteAsync(Question question);
    Task<IQueryable<Question>> ReturnAsync();
    Task<Question?> ReturnAsync(Guid id);
    Task UpdateAsync(Question question);
}
