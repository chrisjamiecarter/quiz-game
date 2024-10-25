using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the <see cref="Quiz"/> entity.
/// Provides methods for creating, updating, deleting, and retrieving data by interacting with the
/// underlying data repositories through the Unit of Work pattern.
/// </summary>
public class QuizService : IQuizService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuizService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(Quiz quiz)
    {
        await _unitOfWork.Quizzes.CreateAsync(quiz);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Quiz quiz)
    {
        await _unitOfWork.Quizzes.DeleteAsync(quiz);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<Quiz>> ReturnAllAsync()
    {
        var answers = await _unitOfWork.Quizzes.ReturnAsync();
        return answers.OrderBy(x => x.Name);
    }

    public async Task<Quiz?> ReturnByIdAsync(Guid id)
    {
        return await _unitOfWork.Quizzes.ReturnAsync(id);
    }

    public async Task<bool> UpdateAsync(Quiz quiz)
    {
        await _unitOfWork.Quizzes.UpdateAsync(quiz);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }
}
