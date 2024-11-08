using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using QuizGame.Application.Repositories;
using QuizGame.Infrastructure.Contexts;

namespace QuizGame.Infrastructure.Repositories;

/// <summary>
/// Provides a central point for managing database transactions and saving changes across repositories.
/// </summary>
/// <remarks>
/// This class follows the Unit of Work design pattern, ensuring that all repository operations 
/// are treated as a single transaction, maintaining data consistency.
/// </remarks>
internal class UnitOfWork : IUnitOfWork
{
    private readonly QuizGameDataContext _context;

    public UnitOfWork(QuizGameDataContext context, 
        IAnswerRepository answerRepository, 
        IGameRepository gameRepository, 
        IQuestionRepository questionRepository, 
        IQuizRepository quizRepository)
    {
        _context = context;
        Answers = answerRepository;
        Games = gameRepository;
        Questions = questionRepository;
        Quizzes = quizRepository;
    }

    public IAnswerRepository Answers { get; }
    
    public IGameRepository Games { get; }

    public IQuestionRepository Questions { get; }

    public IQuizRepository Quizzes { get; }

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            // Database wins - return 0.
            Trace.TraceWarning(exception.Message);
            return 0;
        }
    }
}
