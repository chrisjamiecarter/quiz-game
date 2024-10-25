using FluentAssertions;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;

namespace QuizGame.Api.Tests.Contracts.V1;

public class QuestionMappingsTests
{
    [Fact]
    public void ToDomain_ShouldMapCreateRequestToDomain()
    {
        // Arrange
        var request = new QuestionCreateRequest(Guid.NewGuid(), "Sample Question");

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.QuizId.Should().Be(request.QuizId);
        result.Text.Should().Be(request.Text);
    }

    [Fact]
    public void ToDomain_ShouldMapUpdateRequestToDomain()
    {
        // Arrange
        var originalEntity = new Question
        {
            Id = Guid.NewGuid(),
            QuizId = Guid.NewGuid(),
            Text = "Original Text",
        };

        var updateRequest = new QuestionUpdateRequest("Updated Question");

        // Act
        var result = updateRequest.ToDomain(originalEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(originalEntity.Id);
        result.QuizId.Should().Be(originalEntity.QuizId);
        result.Text.Should().Be(updateRequest.Text);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var domainEntity = new Question
        {
            Id = Guid.NewGuid(),
            QuizId = Guid.NewGuid(),
            Text = "Question Text",
        };

        // Act
        var result = domainEntity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(domainEntity.Id);
        result.QuizId.Should().Be(domainEntity.QuizId);
        result.Text.Should().Be(domainEntity.Text);
    }
}
