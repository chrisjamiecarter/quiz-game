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
        var request = new GameCreateRequest(Guid.NewGuid(), DateTime.UtcNow, Random.Shared.Next(0, 5));

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.QuizId.Should().Be(request.QuizId);
        result.Played.Should().Be(request.Played);
        result.Score.Should().Be(request.Score);
    }

    [Fact]
    public void ToDomain_ShouldMapUpdateRequestToDomain()
    {
        // Arrange
        var originalEntity = new Game
        {
            Id = Guid.NewGuid(),
            QuizId = Guid.NewGuid(),
            Played = DateTime.UtcNow.AddHours(-1),
            Score = Random.Shared.Next(0, 5),
        };

        var updateRequest = new GameUpdateRequest(DateTime.UtcNow, Random.Shared.Next(0, 5));

        // Act
        var result = updateRequest.ToDomain(originalEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(originalEntity.Id);
        result.QuizId.Should().Be(originalEntity.QuizId);
        result.Played.Should().Be(updateRequest.Played);
        result.Score.Should().Be(updateRequest.Score);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var domainEntity = new Game
        {
            Id = Guid.NewGuid(),
            QuizId = Guid.NewGuid(),
            Played = DateTime.UtcNow,
            Score = Random.Shared.Next(0, 5),
        };

        // Act
        var result = domainEntity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(domainEntity.Id);
        result.QuizId.Should().Be(domainEntity.QuizId);
        result.Played.Should().Be(domainEntity.Played);
        result.Score.Should().Be(domainEntity.Score);
    }
}
