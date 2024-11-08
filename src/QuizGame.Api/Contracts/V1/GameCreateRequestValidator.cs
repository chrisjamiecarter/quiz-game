using FluentValidation;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// The validation rules for the <see cref="GameCreateRequest"/> model using FluentValidation. 
/// It ensures that the request data conforms to the expected format before processing.
/// </summary>
public class GameCreateRequestValidator : AbstractValidator<GameCreateRequest>
{
    public GameCreateRequestValidator()
    {
        RuleFor(x => x.QuizId).NotEmpty();
        RuleFor(x => x.Played).NotEmpty();
        RuleFor(x => x.Score).NotEmpty();
    }
}
