namespace QuizGame.Api.Filters;

/// <summary>
/// Provides extension methods to add request validation filters to route handlers in the API. 
/// It simplifies the process of applying validation logic to endpoints
/// by integrating the <see cref="ValidationFilter{TRequest}"/> for request models.
/// </summary>
public static class ValidationExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
