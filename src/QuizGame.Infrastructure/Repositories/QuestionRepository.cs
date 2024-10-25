using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Contexts;

namespace QuizGame.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the <see cref="Question"/> entity.
/// This class implements the <see cref="IQuestionRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class QuestionRepository : IQuestionRepository
{
    private QuizGameDataContext _context;

    public QuestionRepository(QuizGameDataContext context)
    {
        _context = context;
    }

    public Task CreateAsync(Question question)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Question question)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Question>> ReturnAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Question?> ReturnAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Question question)
    {
        throw new NotImplementedException();
    }
}
