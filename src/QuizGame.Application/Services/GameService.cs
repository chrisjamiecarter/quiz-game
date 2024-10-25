using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the <see cref="Game"/> entity.
/// Provides methods for creating, updating, deleting, and retrieving data by interacting with the
/// underlying data repositories through the Unit of Work pattern.
/// </summary>
public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(Game game)
    {
        await _unitOfWork.Games.CreateAsync(game);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Game game)
    {
        await _unitOfWork.Games.DeleteAsync(game);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<Game>> ReturnAllAsync()
    {
        var answers = await _unitOfWork.Games.ReturnAsync();
        return answers.OrderBy(x => x.Played);
    }

    public async Task<Game?> ReturnByIdAsync(Guid id)
    {
        return await _unitOfWork.Games.ReturnAsync(id);
    }

    public async Task<bool> UpdateAsync(Game game)
    {
        await _unitOfWork.Games.UpdateAsync(game);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }
}
