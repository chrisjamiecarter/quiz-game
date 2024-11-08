using FluentValidation;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// The validation rules for the <see cref="AnswerUpdateRequest"/> model using FluentValidation. 
/// It ensures that the request data conforms to the expected format before processing.
/// </summary>
public class AnswerUpdateRequestValidator : AbstractValidator<AnswerUpdateRequest>
{
    public AnswerUpdateRequestValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.IsCorrect).NotEmpty();
    }
}
