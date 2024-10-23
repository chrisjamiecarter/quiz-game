using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IGameService
{
    Task<bool> CreateAsync(Game game);
    Task<Game> ReturnByIdAsync(Guid id);
    Task<IEnumerable<Game>> ReturnAllAsync();
    Task<bool> UpdateAsync(Game game);
    Task<bool> DeleteAsync(Guid id);
}
