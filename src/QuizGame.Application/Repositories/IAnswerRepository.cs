using QuizGame.Domain.Entities;

namespace QuizGame.Application.Repositories;

/// <summary>
/// Defines the contract for performing CRUD operations on <see cref="Answer"/> entities in the
/// data store.
/// </summary>
public interface IAnswerRepository
{
    Task CreateAsync(Answer answer);
    Task DeleteAsync(Answer answer);
    IQueryable<Answer> Return();
    Task<Answer?> ReturnAsync(Guid id);
    Task UpdateAsync(Answer answer);
}
