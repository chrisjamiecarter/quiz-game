using Microsoft.AspNetCore.Mvc;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Api.Endpoints;

/// <summary>
/// Defines endpoints for CRUD operations related to <see cref="Answer"/>.
/// </summary>
public static class AnswerEndpoints
{
    public static async Task<IResult> CreateAnswerAsync([FromBody] AnswerCreateRequest request, [FromServices] IAnswerService service)
    {
        var entity = request.ToDomain();

        var created = await service.CreateAsync(entity);

        return created
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetAnswerAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to create answer." });
    }

    public static async Task<IResult> DeleteAnswerAsync([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete answer." });
    }

    public static async Task<IResult> GetAnswerAsync([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    public static async Task<IResult> GetAnswersAsync([FromServices] IAnswerService service)
    {
        var entities = await service.ReturnAllAsync();
        
        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> GetQuestionAnswersAsync([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        var entities = await service.ReturnByQuestionIdAsync(id);

        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> UpdateAnswerAsync([FromRoute] Guid id, [FromBody] AnswerUpdateRequest request, [FromServices] IAnswerService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedEntity = new Answer
        {
            Id = entity.Id,
            QuestionId = entity.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect,
        };

        var updated = await service.UpdateAsync(updatedEntity);

        return updated
            ? TypedResults.Ok(updatedEntity.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update answer." });
    }
}
