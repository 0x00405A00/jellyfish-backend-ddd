
using Domain.Error;
using Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.Behaviour
{
    public class ValidationPipelingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result

    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPipelingBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            Error[] errors= _validators.
                Select(x=>x.Validate(request)).
                SelectMany(x=>x.Errors).
                Where(x=>x is not null).
                Select(failure=>new Error(failure.ErrorMessage)).
                Distinct().
                ToArray();
            if(errors.Any())
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }
        private static TResult CreateValidationResult<TResult>(Error[] errors) 
            where TResult : Result
        {
            if(typeof(TResult) ==  typeof(Result))
            {
                return (ValidationResult.WithErros(errors) as TResult)!;
            }
            object resultFromValidation = typeof(ValidationResult<>).
                GetGenericTypeDefinition().
                MakeGenericType(typeof(TResult).
                GenericTypeArguments[0]).
                GetMethod(nameof(ValidationResult.WithErros))!.
                Invoke(null, new object?[] { errors })!;

            return (TResult)resultFromValidation;

        }
    }

}
