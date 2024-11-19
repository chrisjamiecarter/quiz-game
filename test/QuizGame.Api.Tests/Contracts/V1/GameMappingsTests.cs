using Azure.Core;
using FluentAssertions;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;

namespace QuizGame.Api.Tests.Contracts.V1;

public class GameMappingsTests
{
    [Fact]
    public void ToDomain_ShouldMapCreateRequestToDomain()
    {
        // Arrange
        var questionCount = 4;
        var request = new GameCreateRequest(Guid.NewGuid(), DateTime.UtcNow, Random.Shared.Next(0, questionCount + 1), questionCount);

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.QuizId.Should().Be(request.QuizId);
        result.Played.Should().Be(request.Played);
        result.Score.Should().Be(request.Score);
        result.MaxScore.Should().Be(request.MaxScore);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var questionCount = 4;
        var entity = new Game
        {
            Id = Guid.NewGuid(),
            QuizId = Guid.NewGuid(),
            Played = DateTime.UtcNow,
            Score = Random.Shared.Next(0, questionCount + 1),
            MaxScore = questionCount,
        };

        // Act
        var result = entity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
        result.QuizId.Should().Be(entity.QuizId);
        result.Played.Should().Be(entity.Played);
        result.Score.Should().Be(entity.Score);
        result.MaxScore.Should().Be(entity.MaxScore);
    }
}
