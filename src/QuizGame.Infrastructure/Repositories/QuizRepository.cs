using Microsoft.EntityFrameworkCore;
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

    public async Task CreateAsync(Quiz quiz)
    {
        await _context.Quiz.AddAsync(quiz);
    }

    public async Task DeleteAsync(Quiz quiz)
    {
        var entity = await _context.Quiz.FindAsync(quiz.Id);
        if (entity is not null)
        {
            _context.Quiz.Remove(entity);
        }
    }

    public async Task<IReadOnlyList<Quiz>> ReturnAsync()
    {
        return await _context.Quiz.ToListAsync();
    }

    public async Task<Quiz?> ReturnAsync(Guid id)
    {
        return await _context.Quiz.FindAsync(id);
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        var entity = await _context.Quiz.FindAsync(quiz.Id);
        if (entity is not null)
        {
            entity.Name = quiz.Name;
            entity.Description = quiz.Description;
            _context.Quiz.Update(entity);
        }
    }
}
