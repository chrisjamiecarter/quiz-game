using Microsoft.AspNetCore.Mvc;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Api.Endpoints;

/// <summary>
/// Defines endpoints for CRUD operations related to <see cref="Question"/>.
/// </summary>
public static class QuestionEndpoints
{
    public static async Task<IResult> CreateQuestionAsync([FromBody] QuestionCreateRequest request, [FromServices] IQuestionService service)
    {
        var entity = request.ToDomain();

        var created = await service.CreateAsync(entity);

        return created
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetQuestionAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to create question." });
    }

    public static async Task<IResult> DeleteQuestionAsync([FromRoute] Guid id, [FromServices] IQuestionService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete question." });
    }

    public static async Task<IResult> GetQuestionAnswersAsync([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        var entities = await service.ReturnByQuestionIdAsync(id);

        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> GetQuestionAsync([FromRoute] Guid id, [FromServices] IQuestionService service)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    public static async Task<IResult> GetQuestionsAsync([FromServices] IQuestionService service)
    {
        var entities = await service.ReturnAllAsync();
        
        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> UpdateQuestionAsync([FromRoute] Guid id, [FromBody] QuestionUpdateRequest request, [FromServices] IQuestionService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedEntity = new Question
        {
            Id = entity.Id,
            QuizId = entity.QuizId,
            Text = request.Text,
        };

        var updated = await service.UpdateAsync(updatedEntity);

        return updated
            ? TypedResults.Ok(updatedEntity.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update question." });
    }
}
