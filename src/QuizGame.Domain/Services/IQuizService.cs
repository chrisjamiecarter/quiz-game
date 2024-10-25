using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IQuizService
{
    Task<bool> CreateAsync(Quiz quiz);
    Task<bool> DeleteAsync(Quiz quiz);
    Task<IEnumerable<Quiz>> ReturnAllAsync();
    Task<Quiz?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Quiz quiz);
}
