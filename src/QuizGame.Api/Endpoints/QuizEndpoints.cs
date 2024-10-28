using Microsoft.AspNetCore.Mvc;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Api.Endpoints;

/// <summary>
/// Defines endpoints for CRUD operations related to <see cref="Quiz"/>.
/// </summary>
public static class QuizEndpoints
{
    public static async Task<IResult> CreateQuizAsync([FromBody] QuizCreateRequest request, [FromServices] IQuizService service)
    {
        var entity = request.ToDomain();

        var created = await service.CreateAsync(entity);

        return created
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetQuizAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to create quiz." });
    }

    public static async Task<IResult> DeleteQuizAsync([FromRoute] Guid id, [FromServices] IQuizService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete quiz." });
    }

    public static async Task<IResult> GetQuizAsync([FromRoute] Guid id, [FromServices] IQuizService service)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    public static async Task<IResult> GetQuizzesAsync([FromServices] IQuizService service)
    {
        var entities = await service.ReturnAllAsync();
        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> UpdateQuizAsync([FromRoute] Guid id, [FromBody] QuizUpdateRequest request, [FromServices] IQuizService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedEntity = request.ToDomain(entity);

        var updated = await service.UpdateAsync(updatedEntity);

        return updated
            ? TypedResults.Ok(updatedEntity.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update quiz." });
    }
}
