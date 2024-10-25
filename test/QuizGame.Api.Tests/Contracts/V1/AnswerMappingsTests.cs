using FluentAssertions;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;

namespace QuizGame.Api.Tests.Contracts.V1;

public class AnswerMappingsTests
{
    [Fact]
    public void ToDomain_ShouldMapCreateRequestToDomain()
    {
        // Arrange
        var request = new AnswerCreateRequest(Guid.NewGuid(), "Sample Answer", true);

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.QuestionId.Should().Be(request.QuestionId);
        result.Text.Should().Be(request.Text);
        result.IsCorrect.Should().Be(request.IsCorrect);
    }

    [Fact]
    public void ToDomain_ShouldMapUpdateRequestToDomain()
    {
        // Arrange
        var originalEntity = new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = Guid.NewGuid(),
            Text = "Original Text",
            IsCorrect = false,
        };

        var updateRequest = new AnswerUpdateRequest("Updated Answer",true);

        // Act
        var result = updateRequest.ToDomain(originalEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(originalEntity.Id);
        result.QuestionId.Should().Be(originalEntity.QuestionId);
        result.Text.Should().Be(updateRequest.Text);
        result.IsCorrect.Should().Be(updateRequest.IsCorrect);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var domainEntity = new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = Guid.NewGuid(),
            Text = "Answer Text",
            IsCorrect = true,
        };

        // Act
        var result = domainEntity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(domainEntity.Id);
        result.QuestionId.Should().Be(domainEntity.QuestionId);
        result.Text.Should().Be(domainEntity.Text);
        result.IsCorrect.Should().Be(domainEntity.IsCorrect);
    }
}
