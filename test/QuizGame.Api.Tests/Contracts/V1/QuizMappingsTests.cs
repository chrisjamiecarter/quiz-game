using Azure.Core;
using FluentAssertions;
using QuizGame.Api.Contracts.V1;
using QuizGame.Domain.Entities;

namespace QuizGame.Api.Tests.Contracts.V1;

public class QuizMappingsTests
{
    [Fact]
    public void ToDomain_ShouldMapCreateRequestToDomain()
    {
        // Arrange
        var request = new QuizCreateRequest("Sample Name", "Sample Description", "Sample ImageUrl");

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(request.Name);
        result.Description.Should().Be(request.Description);
        result.ImageUrl.Should().Be(request.ImageUrl);
    }

    [Fact]
    public void ToDomain_ShouldMapUpdateRequestToDomain()
    {
        // Arrange
        var originalEntity = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original Description",
        };

        var request = new QuizUpdateRequest("Updated Name", "Updated Description", "Updated ImageUrl");

        // Act
        var result = request.ToDomain(originalEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(originalEntity.Id);
        result.Name.Should().Be(request.Name);
        result.Description.Should().Be(request.Description);
        result.ImageUrl.Should().Be(request.ImageUrl);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var entity = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "Quiz Name",
            Description = "Quiz Description",
            ImageUrl = "Quiz ImageUrl",
        };

        // Act
        var result = entity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
        result.Name.Should().Be(entity.Name);
        result.Description.Should().Be(entity.Description);
        result.ImageUrl.Should().Be(entity.ImageUrl);
    }
}
