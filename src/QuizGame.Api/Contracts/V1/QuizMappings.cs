using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class QuizMappings
{
    public static Quiz ToDomain(this QuizCreateRequest request)
    {
        return new Quiz
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
        };
    }

    public static Quiz ToDomain(this QuizUpdateRequest request, Quiz entity)
    {
        return new Quiz
        {
            Id = entity.Id,
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
        };
    }

    public static QuizResponse ToResponse(this Quiz entity)
    {
        return new QuizResponse(entity.Id, entity.Name, entity.Description, entity.ImageUrl);
    }
}
