using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the <see cref="Question"/> entity.
/// Provides methods for creating, updating, deleting, and retrieving data by interacting with the
/// underlying data repositories through the Unit of Work pattern.
/// </summary>
public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(Question question)
    {
        await _unitOfWork.Questions.CreateAsync(question);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Question question)
    {
        await _unitOfWork.Questions.DeleteAsync(question);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<Question>> ReturnAllAsync()
    {
        var questions = await _unitOfWork.Questions.ReturnAsync();
        return questions.OrderBy(_ => Random.Shared.Next());
    }

    public async Task<Question?> ReturnByIdAsync(Guid id)
    {
        return await _unitOfWork.Questions.ReturnAsync(id);
    }

    public async Task<IEnumerable<Question>> ReturnByQuizIdAsync(Guid quizId)
    {
        var questions = await _unitOfWork.Questions.ReturnByQuizIdAsync(quizId);
        return questions.OrderBy(_ => Random.Shared.Next());
    }

    public async Task<bool> UpdateAsync(Question question)
    {
        await _unitOfWork.Questions.UpdateAsync(question);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }
}
