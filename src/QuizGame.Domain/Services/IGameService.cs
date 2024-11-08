using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="Game"/> entities.
/// </summary>
public interface IGameService
{
    Task<bool> CreateAsync(Game game);
    Task<bool> DeleteAsync(Game game);
    Task<(int totalRecords, IEnumerable<Game> gameRecords)> ReturnPaginatedGames(Guid? quizId, string? sortBy, int? pageIndex, int? pageSize);
    Task<IEnumerable<Game>> ReturnAllAsync();
    Task<Game?> ReturnByIdAsync(Guid id);
    Task<IEnumerable<Game>> ReturnByQuizIdAsync(Guid quizId);
    Task<bool> UpdateAsync(Game game);
}
