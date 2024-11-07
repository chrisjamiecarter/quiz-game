using Microsoft.EntityFrameworkCore;
using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Contexts;

namespace QuizGame.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the <see cref="Game"/> entity.
/// This class implements the <see cref="IGameRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class GameRepository : IGameRepository
{
    private QuizGameDataContext _context;

    public GameRepository(QuizGameDataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Game game)
    {
        await _context.Game.AddAsync(game);
    }

    public async Task DeleteAsync(Game game)
    {
        var entity = await _context.Game.FindAsync(game.Id);
        if (entity is not null)
        {
            _context.Game.Remove(entity);
        }
    }

    public async Task<(int totalRecords, IReadOnlyList<Game> gameRecords)> GetPaginatedGames(Guid? quizId, string? sortBy, int pageNumber, int pageSize)
    {
        var query = _context.Game.Include(x => x.Quiz).AsQueryable();

        if (quizId != null)
        {
            query = query.Where(x => x.QuizId == quizId);
        }

        query = sortBy switch
        {
            "played-desc" => query.OrderByDescending(x => x.Played),
            "score" => query.OrderBy(x => x.Score),
            "score-desc" => query.OrderByDescending(x => x.Score),
            _ => query.OrderBy(x => x.Played),
        };

        var totalRecords = await query.CountAsync();
        var gameRecords = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return (totalRecords, gameRecords);
    }

    public async Task<IReadOnlyList<Game>> ReturnAsync()
    {
        return await _context.Game.Include(x => x.Quiz).ToListAsync();
    }

    public async Task<Game?> ReturnAsync(Guid id)
    {
        return await _context.Game.Include(x => x.Quiz).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Game>> ReturnByQuizIdAsync(Guid quizId)
    {
        return await _context.Game.Include(x => x.Quiz).Where(a => a.QuizId == quizId).ToListAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        var entity = await _context.Game.FindAsync(game.Id);
        if (entity is not null)
        {
            entity.Played = game.Played;
            entity.Score = game.Score;
            _context.Game.Update(entity);
        }
    }
}
