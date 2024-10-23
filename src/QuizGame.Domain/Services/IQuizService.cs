using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IQuizService
{
    Task<bool> CreateAsync(Quiz quiz);
    Task<Quiz> ReturnByIdAsync(Guid id);
    Task<IEnumerable<Quiz>> ReturnAllAsync();
    Task<bool> UpdateAsync(Quiz quiz);
    Task<bool> DeleteAsync(Guid id);
}
