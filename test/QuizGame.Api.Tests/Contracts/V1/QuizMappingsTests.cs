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
        var request = new QuizCreateRequest("Sample Name", "Sample Description");

        // Act
        var result = request.ToDomain();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(request.Name);
        result.Description.Should().Be(request.Description);
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

        var updateRequest = new QuizUpdateRequest("Updated Name", "Updated Description");

        // Act
        var result = updateRequest.ToDomain(originalEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(originalEntity.Id);
        result.Name.Should().Be(updateRequest.Name);
        result.Description.Should().Be(updateRequest.Description);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainToResponse()
    {
        // Arrange
        var domainEntity = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "Quiz Name",
            Description = "Quiz Description"
        };

        // Act
        var result = domainEntity.ToResponse();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(domainEntity.Id);
        result.Name.Should().Be(domainEntity.Name);
        result.Description.Should().Be(domainEntity.Description);
    }
}
