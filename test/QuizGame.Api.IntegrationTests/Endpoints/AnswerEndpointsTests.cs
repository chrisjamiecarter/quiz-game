using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Api.Contracts.V1;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Services;

namespace QuizGame.Api.IntegrationTests.Endpoints;

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
    public async Task CreateAnswerAsync_ShouldCreateUser_WhenDataIsValid()
    {
        // Arrange
        var question = await _context.Question.FirstAsync();
        var request = new AnswerCreateRequest(question.Id, "Sample Answer Text", true);

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/quizgame/answers", request);
        var apiResult = await response.Content.ReadFromJsonAsync<AnswerResponse>();
        var dbResult = await _context.Answer.FindAsync(apiResult!.Id);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/v1/quizgame/answers/{apiResult!.Id}");

        apiResult.Should().NotBeNull();
        dbResult.Should().NotBeNull();

        apiResult.Id.Should().NotBeEmpty();
        apiResult.Id.Should().Be(dbResult!.Id);

        apiResult.QuestionId.Should().Be(request.QuestionId);
        apiResult.QuestionId.Should().Be(dbResult.QuestionId);

        apiResult.Text.Should().Be(request.Text);
        apiResult.Text.Should().Be(dbResult.Text);

        apiResult.IsCorrect.Should().Be(request.IsCorrect);
        apiResult.IsCorrect.Should().Be(dbResult.IsCorrect);
    }
}
