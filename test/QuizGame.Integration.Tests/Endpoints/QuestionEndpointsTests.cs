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
public class QuestionEndpointsTests
{
    private readonly HttpClient _client;
    private readonly QuizGameDataContext _context;

    public QuestionEndpointsTests(QuizGameApiFactory factory)
    {
        _client = factory.CreateClient();
        _context = factory.Services.GetRequiredService<QuizGameDataContext>();
        factory.Services.GetRequiredService<ISeederService>().SeedDatabase();
    }

    [Fact]
    public async Task CreateQuestionAsync_ShouldCreate_WhenDataIsValid()
    {
        // Arrange.
        var quiz = await _context.Quiz.AsNoTracking().FirstAsync();
        var request = new QuestionCreateRequest(quiz.Id, "Sample Quiz Text");

        // Act.
        var response = await _client.PostAsJsonAsync($"/api/v1/quizgame/questions", request);
        var apiResult = await response.Content.ReadFromJsonAsync<QuestionResponse>();
        var dbResult = await _context.Question.AsNoTracking().SingleOrDefaultAsync(x => x.Id == apiResult!.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/v1/quizgame/questions/{apiResult!.Id}");

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task DeleteQuestionAsync_ShouldDelete_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Question.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.DeleteAsync($"/api/v1/quizgame/questions/{request.Id}");
        var apiResult = await response.Content.ReadAsStringAsync();
        var dbResult = await _context.Question.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        apiResult.Should().BeEmpty();
        dbResult.Should().BeNull();
    }

    [Fact]
    public async Task GetQuestionAnswersAsync_ShouldGetAnswers_WhenValidQuizId()
    {
        // Arrange.
        var request = await _context.Question.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/questions/{request.Id}/answers");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<AnswerResponse>>();
        var dbResult = await _context.Answer.AsNoTracking().Where(x => x.QuestionId == request.Id).ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task GetQuestionAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.
        var request = await _context.Question.AsNoTracking().FirstAsync();

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/questions/{request.Id}");
        var apiResult = await response.Content.ReadFromJsonAsync<QuestionResponse>();
        var dbResult = await _context.Question.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }

    [Fact]
    public async Task GetQuestionsAsync_ShouldGet_WhenDataIsValid()
    {
        // Arrange.

        // Act.
        var response = await _client.GetAsync($"/api/v1/quizgame/questions");
        var apiResult = await response.Content.ReadFromJsonAsync<IReadOnlyList<QuestionResponse>>();
        var dbResult = await _context.Question.AsNoTracking().ToListAsync();

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeEmpty();
        dbResult.Should().NotBeEmpty();

        apiResult.Should().BeEquivalentTo(dbResult.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task UpdateQuestionAsync_ShouldUpdate_WhenDataIsValid()
    {
        // Arrange.
        var question = await _context.Question.AsNoTracking().FirstAsync();
        var request = new QuestionUpdateRequest($"{question.Text} - Updated");

        // Act.
        var response = await _client.PutAsJsonAsync($"/api/v1/quizgame/questions/{question.Id}", request);
        var apiResult = await response.Content.ReadFromJsonAsync<QuestionResponse>();
        var dbResult = await _context.Question.AsNoTracking().SingleOrDefaultAsync(x => x.Id == question.Id);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Should().BeEquivalentTo(dbResult!.ToResponse());
    }
}
