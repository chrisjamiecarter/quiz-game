using QuizGame.Domain.Entities;

namespace QuizGame.Application.Repositories;

/// <summary>
/// Defines the contract for performing CRUD operations on <see cref="Game"/> entities in the
/// data store.
/// </summary>
public interface IGameRepository
{
    Task CreateAsync(Game game);
    Task DeleteAsync(Game game);
    Task<IQueryable<Game>> ReturnAsync();
    Task<Game?> ReturnAsync(Guid id);
    Task UpdateAsync(Game game);
}
