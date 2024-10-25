using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Contexts;

namespace QuizGame.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the <see cref="Answer"/> entity.
/// This class implements the <see cref="IAnswerRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class AnswerRepository : IAnswerRepository
{
    private QuizGameDataContext _context;

    public AnswerRepository(QuizGameDataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Answer answer)
    {
        await _context.Answer.AddAsync(answer);
    }

    public async Task DeleteAsync(Answer answer)
    {
        var entity = await _context.Answer.FindAsync(answer.Id);
        if (entity is not null)
        {
            _context.Answer.Remove(entity);
        }
    }

    public IQueryable<Answer> Return()
    {
        return _context.Set<Answer>();
    }

    public async Task<Answer?> ReturnAsync(Guid id)
    {
        return await _context.Answer.FindAsync(id);
    }

    public async Task UpdateAsync(Answer answer)
    {
        var entity = await _context.Answer.FindAsync(answer.Id);
        if (entity is not null)
        {
            entity.Text = answer.Text;
            entity.IsCorrect = answer.IsCorrect;
            _context.Answer.Update(entity);
        }
    }
}
