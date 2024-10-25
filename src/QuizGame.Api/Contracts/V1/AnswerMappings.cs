using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class AnswerMappings
{
    public static Answer ToDomain(this AnswerCreateRequest request)
    {
        return new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = request.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect,
        };
    }

    public static Answer ToDomain(this AnswerUpdateRequest request, Answer entity)
    {
        return new Answer
        {
            Id = entity.Id,
            QuestionId = entity.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect,
        };
    }

    public static AnswerResponse ToResponse(this Answer entity)
    {
        return new AnswerResponse(entity.Id, entity.QuestionId, entity.Text, entity.IsCorrect);
    }
}
