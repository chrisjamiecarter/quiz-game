using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Api.Endpoints;

public static class AnswerEndpoints
{
    public static async Task<IResult> CreateAnswer([FromBody] Answer request, [FromServices] IAnswerService service)
    {
        request.Id = Guid.NewGuid();
        var created = await service.CreateAsync(request);

        return created
            ? TypedResults.CreatedAtRoute(request, nameof(GetAnswer), new { id = request.Id })
            : TypedResults.BadRequest(new { error = "Unable to create answer." });
    }

    public static async Task<IResult> DeleteAnswer([FromRoute] Guid id, [FromServices] IAnswerService service)
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

    public static async Task<IResult> GetAnswer([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity);
    }

    public static IResult GetAnswers([FromServices] IAnswerService service)
    {
        var entities = service.ReturnAll();
        return TypedResults.Ok(entities);
    }

    public static IResult GetQuestionAnswers([FromRoute] Guid id, [FromServices] IAnswerService service)
    {
        // TODO:
        var entities = service.ReturnAll().Where(x => x.QuestionId == id);
        
        return TypedResults.Ok(entities);
    }

    public static async Task<IResult> UpdateAnswer([FromRoute] Guid id, [FromBody] Answer request, [FromServices] IAnswerService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedAnswer = new Answer
        {
            Id = entity.Id,
            QuestionId = entity.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect,
        };

        var updated = await service.UpdateAsync(updatedAnswer);

        return updated
            ? TypedResults.Ok(updatedAnswer)
            : TypedResults.BadRequest(new { error = "Unable to update answer." });
    }
}
