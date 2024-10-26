using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Api.Contracts.V1;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Services;
using QuizGame.Integration.Tests.Factories;

namespace QuizGame.Integration.Tests.Endpoints;

[Collection(nameof(QuizGameApiFactoryCollection))]
public class GameEndpointsTests
{
    private readonly HttpClient _client;
    private readonly QuizGameDataContext _context;

    public GameEndpointsTests(QuizGameApiFactory factory)
    {
        _client = factory.CreateClient();
        _context = factory.Services.GetRequiredService<QuizGameDataContext>();
        factory.Services.GetRequiredService<ISeederService>().SeedDatabase();
    }

    [Fact]
    public async Task CreateGameAsync_ShouldCreate_WhenDataIsValid()
    {
        // Arrange.
        var quiz = await _context.Quiz.AsNoTracking().FirstAsync();
        var questionsCount = await _context.Question.AsNoTracking().Where(x => x.QuizId == quiz.Id).CountAsync();
        var request = new GameCreateRequest(quiz.Id, DateTime.Now, Random.Shared.Next(questionsCount + 1));

        // Act.
        var response = await _client.PostAsJsonAsync($"/api/v1/quizgame/games", request);
        var apiResult = await response.Content.ReadFromJsonAsync<GameResponse>();
        var dbResult = await _context.Game.AsNoTracking().SingleOrDefaultAsync(x => x.Id == apiResult!.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/v1/quizgame/games/{apiResult!.Id}");

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task DeleteGameAsync_ShouldDelete_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Game.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.DeleteAsync($"/api/v1/quizgame/games/{request.Id}");
        var apiResult = await response.Content.ReadAsStringAsync();
        var dbResult = await _context.Game.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        apiResult.Should().BeEmpty();
        dbResult.Should().BeNull();
    }

    [Fact]
    public async Task GetGameAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Game.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/games/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<GameResponse>();
        var dbResult = await _context.Game.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task GetGamesAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/games");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<GameResponse>>();
        var dbResult = await _context.Game.AsNoTracking().ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task GetQuizGamesAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Quiz.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/games/quiz/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<GameResponse>>();
        var dbResult = await _context.Game.AsNoTracking().Where(x => x.QuizId == request.Id).ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task UpdateGameAsync_ShouldUpdate_WhenDataIsValid()
    {
        // Arrange.
        var game = await _context.Game.AsNoTracking().FirstAsync();
        var questionsCount = await _context.Question.AsNoTracking().Where(x => x.QuizId == game.QuizId).CountAsync();
        var request = new GameUpdateRequest(DateTime.Now, Random.Shared.Next(questionsCount + 1));

        // Act.
        var response = await _client.PutAsJsonAsync($"/api/v1/quizgame/games/{game.Id}", request);
        var apiResult = await response.Content.ReadFromJsonAsync<GameResponse>();
        var dbResult = await _context.Game.AsNoTracking().SingleOrDefaultAsync(x => x.Id == game.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }
}
