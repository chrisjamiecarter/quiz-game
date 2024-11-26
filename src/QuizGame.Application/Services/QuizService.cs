using Microsoft.Extensions.Configuration;
using QuizGame.Application.Repositories;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the <see cref="Quiz"/> entity.
/// Provides methods for creating, updating, deleting, and retrieving quiz data with optimized caching
/// strategies to improve read performance, and database interactions through the underlying data 
/// repositories through the Unit of Work pattern.
/// </summary>
public class QuizService : IQuizService
{
    private readonly string AllQuizzesCacheKey = "quiz_all";

    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public QuizService(ICacheService cacheService, IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(Quiz quiz)
    {
        var cacheKey = $"quiz_{quiz.Id}";
        await _cacheService.SetAsync(cacheKey, quiz);
        await _cacheService.RemoveAsync(AllQuizzesCacheKey);

        await _unitOfWork.Quizzes.CreateAsync(quiz);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Quiz quiz)
    {
        var cacheKey = $"quiz_{quiz.Id}";
        await _cacheService.RemoveAsync(cacheKey);
        await _cacheService.RemoveAsync(AllQuizzesCacheKey);

        await _unitOfWork.Quizzes.DeleteAsync(quiz);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<Quiz>> ReturnAllAsync()
    {
        var cachedData = await _cacheService.GetAsync<IEnumerable<Quiz>>(AllQuizzesCacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }

        var quizzes = await _unitOfWork.Quizzes.ReturnAsync();
        await _cacheService.SetAsync(AllQuizzesCacheKey, quizzes);
        return quizzes;
    }

    public async Task<Quiz?> ReturnByIdAsync(Guid id)
    {
        var cacheKey = $"quiz_{id}";
        var cachedQuiz = await _cacheService.GetAsync<Quiz?>(cacheKey);
        if (cachedQuiz != null)
        {
            return cachedQuiz;
        }

        var cachedData = await _cacheService.GetAsync<IEnumerable<Quiz>>(AllQuizzesCacheKey);
        if (cachedData != null)
        {
            return cachedData.FirstOrDefault(quiz => quiz.Id == id);
        }

        var quiz = await _unitOfWork.Quizzes.ReturnAsync(id);
        await _cacheService.SetAsync(cacheKey, quiz);
        return quiz;
    }

    public async Task<bool> UpdateAsync(Quiz quiz)
    {
        var cacheKey = $"quiz_{quiz.Id}";
        await _cacheService.SetAsync(cacheKey, quiz);
        await _cacheService.RemoveAsync(AllQuizzesCacheKey);

        await _unitOfWork.Quizzes.UpdateAsync(quiz);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }
}
