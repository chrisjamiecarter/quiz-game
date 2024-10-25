using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the <see cref="Answer"/> entity.
/// Provides methods for creating, updating, deleting, and retrieving data by interacting with the
/// underlying data repositories through the Unit of Work pattern.
/// </summary>
public class AnswerService : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnswerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(Answer answer)
    {
        await _unitOfWork.Answers.CreateAsync(answer);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Answer answer)
    {
        await _unitOfWork.Answers.DeleteAsync(answer);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<Answer>> ReturnAllAsync()
    {
        var answers = await _unitOfWork.Answers.ReturnAsync();
        return answers.OrderBy(x => Random.Shared.Next());
    }

    public async Task<Answer?> ReturnByIdAsync(Guid id)
    {
        return await _unitOfWork.Answers.ReturnAsync(id);
    }

    public async Task<bool> UpdateAsync(Answer answer)
    {
        await _unitOfWork.Answers.UpdateAsync(answer);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }
}
