using Microsoft.AspNetCore.Mvc;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Services;

namespace QuizGame.Api.Endpoints;

/// <summary>
/// Defines endpoints for CRUD operations related to <see cref="Game"/>.
/// </summary>
public static class GameEndpoints
{
    public static async Task<IResult> CreateGameAsync([FromBody] GameCreateRequest request, [FromServices] IGameService service)
    {
        var entity = request.ToDomain();

        var created = await service.CreateAsync(entity);

        return created
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetGameAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to create game." });
    }

    public static async Task<IResult> DeleteGameAsync([FromRoute] Guid id, [FromServices] IGameService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete game." });
    }

    public static async Task<IResult> GetGameAsync([FromRoute] Guid id, [FromServices] IGameService service)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    public static async Task<IResult> GetGamesAsync([FromServices] IGameService service)
    {
        var entities = await service.ReturnAllAsync();

        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> GetPaginatedGamesAsync([FromServices] IGameService service,
                                                             [FromQuery] Guid? quizId = null,
                                                             [FromQuery] string? sortBy = null,
                                                             [FromQuery] int? page = null,
                                                             [FromQuery] int? size = null)
    {
        if (page < 1 || size < 1)
        {
            return TypedResults.BadRequest(new { error = "Invalid query parameters." });
        }

        var (totalRecords, gameRecords) = await service.GetPaginatedGames(quizId, sortBy, page, size);

        return TypedResults.Ok(new PaginatedGameResponse(totalRecords, gameRecords.Select(x => x.ToResponse()).ToList()));
    }

    public static async Task<IResult> GetQuizGamesAsync([FromRoute] Guid id, [FromServices] IGameService service)
    {
        var entities = await service.ReturnByQuizIdAsync(id);

        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    public static async Task<IResult> UpdateGameAsync([FromRoute] Guid id, [FromBody] GameUpdateRequest request, [FromServices] IGameService service)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedEntity = new Game
        {
            Id = entity.Id,
            QuizId = entity.QuizId,
            Played = request.Played,
            Score = request.Score,
        };

        var updated = await service.UpdateAsync(updatedEntity);

        return updated
            ? TypedResults.Ok(updatedEntity.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update game." });
    }
}
