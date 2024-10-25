using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer);
    Task<bool> DeleteAsync(Answer answer);
    IEnumerable<Answer> ReturnAll();
    Task<Answer?> ReturnByIdAsync(Guid id);
    Task<bool> UpdateAsync(Answer answer);
}
