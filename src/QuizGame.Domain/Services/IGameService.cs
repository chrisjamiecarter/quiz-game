using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IGameService
{
    Task<bool> CreateAsync(Game game);
    Task<bool> DeleteAsync(Game game);
    Task<IEnumerable<Game>> ReturnAllAsync();
    Task<Game?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Game game);
}
