using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
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

    public async Task CreateAsync(Question question)
    {
        await _context.Question.AddAsync(question);
    }

    public async Task DeleteAsync(Question question)
    {
        var entity = await _context.Question.FindAsync(question.Id);
        if (entity is not null)
        {
            _context.Question.Remove(entity);
        }
    }

    public async Task<IReadOnlyList<Question>> ReturnAsync()
    {
        return await _context.Question.ToListAsync();
    }

    public async Task<Question?> ReturnAsync(Guid id)
    {
        return await _context.Question.FindAsync(id);
    }

    public async Task UpdateAsync(Question question)
    {
        var entity = await _context.Question.FindAsync(question.Id);
        if (entity is not null)
        {
            entity.Text = question.Text;
            _context.Question.Update(entity);
        }
    }
}
