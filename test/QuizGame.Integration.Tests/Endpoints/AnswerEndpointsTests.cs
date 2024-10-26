using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Api.Contracts.V1;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Services;

namespace QuizGame.Integration.Tests.Endpoints;

public class AnswerEndpointsTests : IClassFixture<QuizGameApiFactory>
{
    private readonly HttpClient _client;
    private readonly QuizGameDataContext _context;

    public AnswerEndpointsTests(QuizGameApiFactory factory)
    {
        _client = factory.CreateClient();
        _context = factory.Services.GetRequiredService<QuizGameDataContext>();
        factory.Services.GetRequiredService<ISeederService>().SeedDatabase();
    }

    [Fact]
    public async Task CreateAnswerAsync_ShouldCreate_WhenDataIsValid()
    {
        // Arrange.
        var question = await _context.Question.AsNoTracking().FirstAsync();
        var request = new AnswerCreateRequest(question.Id, "Sample Answer Text", true);

        // Act.
        var response = await _client.PostAsJsonAsync($"/api/v1/quizgame/answers", request);
        var apiResult = await response.Content.ReadFromJsonAsync<AnswerResponse>();
        var dbResult = await _context.Answer.AsNoTracking().SingleOrDefaultAsync(x => x.Id == apiResult!.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/v1/quizgame/answers/{apiResult!.Id}");

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task DeleteAnswerAsync_ShouldDelete_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Answer.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.DeleteAsync($"/api/v1/quizgame/answers/{request.Id}");
        var apiResult = await response.Content.ReadAsStringAsync();
        var dbResult = await _context.Answer.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        apiResult.Should().BeEmpty();
        dbResult.Should().BeNull();
    }

    [Fact]
    public async Task GetAnswerAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Answer.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/answers/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<AnswerResponse>();
        var dbResult = await _context.Answer.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task GetAnswersAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/answers");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<AnswerResponse>>();
        var dbResult = await _context.Answer.AsNoTracking().ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task GetQuestionAnswersAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Question.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/answers/question/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<AnswerResponse>>();
        var dbResult = await _context.Answer.AsNoTracking().Where(x => x.QuestionId == request.Id).ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task UpdateAnswerAsync_ShouldUpdate_WhenDataIsValid()
    {
        // Arrange.
        var answer = await _context.Answer.AsNoTracking().FirstAsync();
        var request = new AnswerUpdateRequest($"{answer.Text} - Updated", !answer.IsCorrect);

        // Act.
        var response = await _client.PutAsJsonAsync($"/api/v1/quizgame/answers/{answer.Id}", request);
        var apiResult = await response.Content.ReadFromJsonAsync<AnswerResponse>();
        var dbResult = await _context.Answer.AsNoTracking().SingleOrDefaultAsync(x => x.Id == answer.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }
}
