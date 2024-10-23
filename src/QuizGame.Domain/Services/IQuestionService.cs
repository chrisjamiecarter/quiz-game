using QuizGame.Domain.Entities;

namespace QuizGame.Domain.Services;

public interface IQuestionService
{
    Task<bool> CreateAsync(Question question);
    Task<Question> ReturnByIdAsync(Guid id);
    Task<IEnumerable<Question>> ReturnAllAsync();
    Task<bool> UpdateAsync(Question question);
    Task<bool> DeleteAsync(Guid id);
}
