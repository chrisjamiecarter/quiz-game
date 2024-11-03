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
    Task<(int totalRecords, IReadOnlyList<Game> gameRecords)> GetPaginatedGames(Guid? quizId, string? sortBy, int pageNumber, int pageSize);
    Task<IReadOnlyList<Game>> ReturnAsync();
    Task<Game?> ReturnAsync(Guid id);
    Task<IReadOnlyList<Game>> ReturnByQuizIdAsync(Guid quizId);
    Task UpdateAsync(Game game);
}
