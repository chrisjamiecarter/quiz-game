using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="Game"/> entities.
/// </summary>
public interface IGameService
{
    Task<bool> CreateAsync(Game game);
    Task<bool> DeleteAsync(Game game);
    Task<IEnumerable<Game>> ReturnAllAsync();
    Task<Game?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Game game);
}
