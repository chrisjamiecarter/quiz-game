using FluentValidation;

namespace QuizGame.Api.Contracts.V1;

/// <summary>
/// The validation rules for the <see cref="QuestionUpdateRequest"/> model using FluentValidation. 
/// It ensures that the request data conforms to the expected format before processing.
/// </summary>
public class QuestionUpdateRequestValidator : AbstractValidator<QuestionUpdateRequest>
{
    public QuestionUpdateRequestValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
    }
}
