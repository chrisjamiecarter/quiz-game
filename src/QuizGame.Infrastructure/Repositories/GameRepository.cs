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

    public Task CreateAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Game>> ReturnAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Game?> ReturnAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Game game)
    {
        throw new NotImplementedException();
    }
}
