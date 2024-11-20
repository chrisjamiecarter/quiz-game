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
public class QuizEndpointsTests
{
    private readonly HttpClient _client;
    private readonly QuizGameDataContext _context;

    public QuizEndpointsTests(QuizGameApiFactory factory)
    {
        _client = factory.CreateClient();
        _context = factory.Services.GetRequiredService<QuizGameDataContext>();
        factory.Services.GetRequiredService<ISeederService>().SeedDatabase();
    }

    [Fact]
    public async Task CreateQuizAsync_ShouldCreate_WhenDataIsValid()
    {
        // Arrange.
        var quiz = await _context.Quiz.AsNoTracking().FirstAsync();
        var request = new QuizCreateRequest("Sample Quiz Name", "Sample Quiz Description", "Sample Quiz ImageUrl");

        // Act.
        var response = await _client.PostAsJsonAsync($"/api/v1/quizgame/quizzes", request);
        var apiResult = await response.Content.ReadFromJsonAsync<QuizResponse>();
        var dbResult = await _context.Quiz.AsNoTracking().SingleOrDefaultAsync(x => x.Id == apiResult!.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/v1/quizgame/quizzes/{apiResult!.Id}");

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task DeleteQuizAsync_ShouldDelete_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Quiz.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.DeleteAsync($"/api/v1/quizgame/quizzes/{request.Id}");
        var apiResult = await response.Content.ReadAsStringAsync();
        var dbResult = await _context.Quiz.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        apiResult.Should().BeEmpty();
        dbResult.Should().BeNull();
    }

    [Fact]
    public async Task GetQuizAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Quiz.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/quizzes/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<QuizResponse>();
        var dbResult = await _context.Quiz.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task GetQuizzesAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/quizzes");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<QuizResponse>>();
        var dbResult = await _context.Quiz.AsNoTracking().ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task UpdateQuizAsync_ShouldUpdate_WhenDataIsValid()
    {
        // Arrange.
        var quiz = await _context.Quiz.AsNoTracking().FirstAsync();
        var request = new QuizUpdateRequest($"{quiz.Name} - Updated", $"{quiz.Description} - Updated", $"{quiz.ImageUrl} - Updated");

        // Act.
        var response = await _client.PutAsJsonAsync($"/api/v1/quizgame/quizzes/{quiz.Id}", request);
        var apiResult = await response.Content.ReadFromJsonAsync<QuizResponse>();
        var dbResult = await _context.Quiz.AsNoTracking().SingleOrDefaultAsync(x => x.Id == quiz.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }
}
