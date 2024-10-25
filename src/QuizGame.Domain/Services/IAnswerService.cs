using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer);
    Task<bool> DeleteAsync(Answer answer);
    Task<IEnumerable<Answer>> ReturnAllAsync();
    Task<Answer?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Answer answer);
}
