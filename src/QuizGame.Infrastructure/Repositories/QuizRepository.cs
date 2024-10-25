using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Contexts;

namespace QuizGame.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the <see cref="Quiz"/> entity.
/// This class implements the <see cref="IQuizRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class QuizRepository : IQuizRepository
{
    private QuizGameDataContext _context;

    public QuizRepository(QuizGameDataContext context)
    {
        _context = context;
    }

    public Task CreateAsync(Quiz quiz)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Quiz quiz)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Quiz>> ReturnAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Quiz?> ReturnAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Quiz quiz)
    {
        throw new NotImplementedException();
    }
}
