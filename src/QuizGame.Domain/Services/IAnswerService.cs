using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer);
    Task<Answer> ReturnByIdAsync(Guid id);
    Task<IEnumerable<Answer>> ReturnAllAsync();
    Task<bool> UpdateAsync(Answer answer);
    Task<bool> DeleteAsync(Guid id);
}
