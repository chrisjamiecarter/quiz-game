using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class QuestionMappings
{
    public static Question ToDomain(this QuestionCreateRequest request)
    {
        return new Question
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Text = request.Text,
        };
    }

    public static Question ToDomain(this QuestionUpdateRequest request, Question entity)
    {
        return new Question
        {
            Id = entity.Id,
            QuizId = entity.QuizId,
            Text = request.Text,
        };
    }

    public static QuestionResponse ToResponse(this Question entity)
    {
        return new QuestionResponse(entity.Id, entity.QuizId, entity.Text);
    }
}
