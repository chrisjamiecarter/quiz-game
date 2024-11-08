using FluentValidation;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// The validation rules for the <see cref="QuizUpdateRequest"/> model using FluentValidation. 
/// It ensures that the request data conforms to the expected format before processing.
/// </summary>
public class QuizUpdateRequestValidator : AbstractValidator<QuizUpdateRequest>
{
    public QuizUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
