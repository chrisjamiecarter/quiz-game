using QuizGame.Domain.Entities;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between domain entities and request/response models.
/// </summary>
public static class QuizMappings
{
    public static Quiz ToDomain(this QuizCreateRequest request)
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
        };

        if (!string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            quiz.ImageUrl = request.ImageUrl;
        }
        
        return quiz;
    }

    public static Quiz ToDomain(this QuizUpdateRequest request, Quiz entity)
    {
        var quiz = new Quiz
        {
            Id = entity.Id,
            Name = request.Name,
            Description = request.Description,
        };

        if (!string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            quiz.ImageUrl = request.ImageUrl;
        }

        return quiz;
    }

    public static QuizResponse ToResponse(this Quiz entity)
    {
        return new QuizResponse(entity.Id, entity.Name, entity.Description, entity.ImageUrl);
    }
}
